using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        #region AsyncEnumerable Reading
        
        /// <summary>
        /// Open a new Connection on the database and execute the SELECT query asynchronously
        /// The connection will automatically be disposed after reading
        /// </summary>
        /// <param name="database">Data source for connecting to mysql</param>
        /// <param name="query">SQL Query string</param>
        /// <returns>Returns an AsyncEnumerable, where each iteration returns a readable row from the query</returns>
        public static IAsyncEnumerable<MySqlDataReader> ReadAsync( this MySqlDataSource database, string query )
            => database.ReadAsync(query, []);
        
        /// <summary>
        /// Open a new Connection on the database and execute the SELECT query asynchronously
        /// The connection will automatically be disposed after reading
        /// </summary>
        /// <param name="database">Data source for connecting to mysql</param>
        /// <param name="query">SQL Query string</param>
        /// <param name="parameters">Enumerable set of parameters to be substituted into the query string</param>
        /// <returns>Returns an AsyncEnumerable, where each iteration returns a readable row from the query</returns>
        public static IAsyncEnumerable<MySqlDataReader> ReadAsync( this MySqlDataSource database, string query, IEnumerable<MySqlParameter> parameters ) {
            MySqlCommand command = new(query);
            command.Parameters.AddRange(parameters);
            return new AsyncDataReaderEnumerable(database, null, command);
        }
        
        /// <summary>
        /// Execute a query on an existing database connection
        /// </summary>
        /// <param name="connection">A database connection</param>
        /// <param name="query">SQL Query string</param>
        /// <returns>Returns an AsyncEnumerable, where each iteration returns a readable row from the query</returns>
        public static IAsyncEnumerable<MySqlDataReader> ReadAsync( this MySqlConnection connection, string query )
            => connection.ReadAsync(query, []);
        
        /// <summary>
        /// Execute a query on an existing database connection
        /// </summary>
        /// <param name="connection">A database connection</param>
        /// <param name="query">SQL Query string</param>
        /// <param name="parameters">Enumerable set of parameters to be substituted into the query string</param>
        /// <returns>Returns an AsyncEnumerable, where each iteration returns a readable row from the query</returns>
        public static IAsyncEnumerable<MySqlDataReader> ReadAsync( this MySqlConnection connection, string query, IEnumerable<MySqlParameter> parameters ) {
            MySqlCommand command = new(query);
            
            command.Parameters.AddRange(parameters);
            
            return new AsyncDataReaderEnumerable(null, connection, command );
        }
        
        /// <summary>
        /// Execute a query on an existing transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="query">SQL Query string</param>
        /// <returns>Returns an AsyncEnumerable, where each iteration returns a readable row from the query</returns>
        public static IAsyncEnumerable<MySqlDataReader> ReadAsync( this MySqlTransaction transaction, string query )
            => transaction.ReadAsync(query, []);
        
        /// <summary>
        /// Execute a query on an existing transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="query">SQL Query string</param>
        /// <param name="parameters">Enumerable set of parameters to be substituted into the query string</param>
        /// <returns>Returns an AsyncEnumerable, where each iteration returns a readable row from the query</returns>
        public static IAsyncEnumerable<MySqlDataReader> ReadAsync( this MySqlTransaction transaction, string query, IEnumerable<MySqlParameter> parameters ) {
            MySqlCommand command = new(query, transaction.Connection, transaction);
            
            command.Parameters.AddRange(parameters);
            
            return new AsyncDataReaderEnumerable(null, transaction.Connection, command );
        }
        
        private sealed class AsyncDataReaderEnumerable : IAsyncEnumerable<MySqlDataReader> {
            private readonly MySqlDataSource? Database;
            private readonly MySqlConnection? Connection;
            private readonly MySqlCommand Command;
            
            public AsyncDataReaderEnumerable(
                MySqlDataSource? database,
                MySqlConnection? connection,
                MySqlCommand command
            ) {
                this.Database = database;
                this.Connection = connection;
                this.Command = command;
            }
            
            /// <inheritdoc />
            public IAsyncEnumerator<MySqlDataReader> GetAsyncEnumerator( CancellationToken cancellationToken = default )
                => new AsyncDataReaderEnumerator(this.Command, this.Connection, this.Database, cancellationToken);
        }
        
        private sealed class AsyncDataReaderEnumerator : IAsyncEnumerator<MySqlDataReader> {
            private readonly CancellationToken CancellationToken;
            private readonly bool CloseConnection;
            
            private readonly MySqlDataSource? Database;
            private readonly MySqlCommand Command;
            
            private MySqlConnection? Connection { get => this.Command.Connection; set => this.Command.Connection = value; }
            private MySqlDataReader? Reader;
            
            public AsyncDataReaderEnumerator(
                MySqlCommand command,
                MySqlConnection? connection = null,
                MySqlDataSource? database = null,
                CancellationToken cancellation = default
            ) {
                if ( database is null && connection is null ) {
                    throw new InvalidOperationException("Must provide a datasource or a datasource connection");
                }
                
                this.Command = command;
                this.Database = database;
                this.Connection = connection;
                this.CancellationToken = cancellation;
                this.CloseConnection = database is not null;
            }
            
            /// <inheritdoc />
            public MySqlDataReader Current => this.Reader ?? throw new Exception("Accessing datareader in an invalid state");
            
            /// <inheritdoc />
            public async ValueTask<bool> MoveNextAsync() {
                this.CancellationToken.ThrowIfCancellationRequested();
                
                // Open a new connection to the database if we're missing one
                if ( this.Connection is null ) {
                    if ( this.Database is null ) {
                        throw new InvalidOperationException("Missing database in which to open a connection with");
                    }
                    
                    this.Connection = await this.Database.OpenConnectionAsync(this.CancellationToken);
                }
                
                // Execute the query and begin reading if we haven't
                this.Reader ??= await this.Command.ExecuteReaderAsync(this.CancellationToken);
                
                // Read the new row
                return await this.Reader.ReadAsync(this.CancellationToken);
            }
            
            /// <inheritdoc />
            public async ValueTask DisposeAsync() {
                if ( this.Reader is {} reader ) {
                    await reader.DisposeAsync();
                }
                
                await this.Command.DisposeAsync();
                
                if ( this.CloseConnection && this.Connection is {} connection ) {
                    await connection.DisposeAsync();
                }
            }
        }
        
        #endregion
    }
}
