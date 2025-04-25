using System;
using System.Collections;
using System.Collections.Generic;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        /// <summary>
        /// Open a new Connection on the database and execute the SELECT query
        /// The connection will automatically be disposed after reading
        /// </summary>
        /// <param name="database">Data source for connecting to mysql</param>
        /// <param name="query">SQL Query string</param>
        /// <returns>Returns an Enumerable, where each iteration returns a readable row from the query</returns>
        public static IEnumerable<MySqlDataReader> Read( this MySqlDataSource database, string query )
            => database.Read(query, []);
        
        /// <summary>
        /// Open a new Connection on the database and execute the SELECT query
        /// The connection will automatically be disposed after reading
        /// </summary>
        /// <param name="database">Data source for connecting to mysql</param>
        /// <param name="query">SQL Query string</param>
        /// <param name="parameters">Enumerable set of parameters to be substituted into the query string</param>
        /// <returns>Returns an Enumerable, where each iteration returns a readable row from the query</returns>
        public static IEnumerable<MySqlDataReader> Read( this MySqlDataSource database, string query, IEnumerable<MySqlParameter> parameters ) {
            MySqlCommand command = new(query);
            command.Parameters.AddRange(parameters);
            return new DataReaderEnumerable(database, null, command);
        }
        
        /// <summary>
        /// Execute a query on an existing database connection
        /// </summary>
        /// <param name="connection">A database connection</param>
        /// <param name="query">SQL Query string</param>
        /// <returns>Returns an Enumerable, where each iteration returns a readable row from the query</returns>
        public static IEnumerable<MySqlDataReader> Read( this MySqlConnection connection, string query )
            => connection.Read(query, []);
        
        /// <summary>
        /// Execute a query on an existing database connection
        /// </summary>
        /// <param name="connection">A database connection</param>
        /// <param name="query">SQL Query string</param>
        /// <param name="parameters">Enumerable set of parameters to be substituted into the query string</param>
        /// <returns>Returns an Enumerable, where each iteration returns a readable row from the query</returns>
        public static IEnumerable<MySqlDataReader> Read( this MySqlConnection connection, string query, IEnumerable<MySqlParameter> parameters ) {
            MySqlCommand command = new(query);
            
            command.Parameters.AddRange(parameters);
            
            return new DataReaderEnumerable(null, connection, command );
        }
        
        /// <summary>
        /// Execute a query on an existing transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="query">SQL Query string</param>
        /// <returns>Returns an Enumerable, where each iteration returns a readable row from the query</returns>
        public static IEnumerable<MySqlDataReader> Read( this MySqlTransaction transaction, string query )
            => transaction.Read(query, []);
        
        /// <summary>
        /// Execute a query on an existing transaction
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="query">SQL Query string</param>
        /// <param name="parameters">Enumerable set of parameters to be substituted into the query string</param>
        /// <returns>Returns an Enumerable, where each iteration returns a readable row from the query</returns>
        public static IEnumerable<MySqlDataReader> Read( this MySqlTransaction transaction, string query, IEnumerable<MySqlParameter> parameters ) {
            MySqlCommand command = new(query, transaction.Connection, transaction);
            
            command.Parameters.AddRange(parameters);
            
            return new DataReaderEnumerable(null, transaction.Connection, command );
        }
        
        private sealed class DataReaderEnumerable : IEnumerable<MySqlDataReader> {
            private readonly MySqlDataSource? Database;
            private readonly MySqlConnection? Connection;
            private readonly MySqlCommand Command;
            
            public DataReaderEnumerable(
                MySqlDataSource? database,
                MySqlConnection? connection,
                MySqlCommand command
            ) {
                this.Database = database;
                this.Connection = connection;
                this.Command = command;
            }
            
            /// <inheritdoc />
            IEnumerator IEnumerable.GetEnumerator()
                => this.GetEnumerator();
            
            /// <inheritdoc />
            public IEnumerator<MySqlDataReader> GetEnumerator()
                => new DataReaderEnumerator(this.Command, this.Connection, this.Database);
        }
        
        private sealed class DataReaderEnumerator : IEnumerator<MySqlDataReader> {
            private readonly bool CloseConnection;
            
            private readonly MySqlDataSource? Database;
            private readonly MySqlCommand Command;
            
            private MySqlConnection? Connection { get => this.Command.Connection; set => this.Command.Connection = value; }
            private MySqlDataReader? Reader;
            
            public DataReaderEnumerator(
                MySqlCommand command,
                MySqlConnection? connection = null,
                MySqlDataSource? database = null
            ) {
                if ( database is null && connection is null ) {
                    throw new InvalidOperationException("Must provide a datasource or a datasource connection");
                }
                
                this.Command = command;
                this.Database = database;
                this.Connection = connection;
                this.CloseConnection = database is not null;
            }
            
            /// <inheritdoc />
            object? IEnumerator.Current => this.Reader;
            
            /// <inheritdoc />
            public MySqlDataReader Current => this.Reader ?? throw new Exception("Accessing datareader in an invalid state");
            
            /// <inheritdoc />
            public bool MoveNext() {
                // Open a new connection to the database if we're missing one
                if ( this.Connection is null ) {
                    if ( this.Database is null ) {
                        throw new InvalidOperationException("Missing database in which to open a connection with");
                    }
                    
                    this.Connection = this.Database.OpenConnection();
                }
                
                // Execute the query and begin reading if we haven't
                this.Reader ??= this.Command.ExecuteReader();
                
                // Read the new row
                return this.Reader.Read();
            }
            
            /// <inheritdoc />
            public void Reset() {
                if ( this.Reader is {} reader ) {
                    // Dispose of the current reader
                    reader.Dispose();
                    
                    // Reset the reader to NULL to allow a new reader
                    this.Reader = null;
                }
            }
            
            /// <inheritdoc />
            public void Dispose() {
                if ( this.Reader is {} reader ) {
                    reader.Dispose();
                }
                
                this.Command.Dispose();
                
                if ( this.CloseConnection && this.Connection is {} connection ) {
                    connection.Dispose();
                }
            }
        }
    }
}
