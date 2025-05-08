using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class NonQuery {
        #region Command
        
        public static Task<int> ExecuteNonQueryAsync( this MySqlCommand command, IEnumerable<MySqlParameter> parameters, CancellationToken cancellation = default ) {
            command.Parameters.Set(parameters);
            return command.ExecuteNonQueryAsync(cancellation);
        }
        
        public static async Task<int> ExecuteNonQueryAsync( this MySqlCommand command, IEnumerable<IEnumerable<MySqlParameter>> parameterSet, CancellationToken cancellation = default ) {
            int output = 0;
            
            foreach ( IEnumerable<MySqlParameter> parameters in parameterSet ) {
                output += await command.ExecuteNonQueryAsync(parameters, cancellation);
            }
            
            return output;
        }
        
        #endregion
        #region Source
        
        public static async Task<int> ExecuteNonQueryAsync( this MySqlDataSource database, string query, Action<MySqlCommand>? func = null, CancellationToken cancellation = default ) {
            await using ( MySqlConnection connection = await database.OpenConnectionAsync(cancellation) ) {
                return await connection.ExecuteNonQueryAsync(query, func, cancellation);
            }
        }
        
        public static Task<int> ExecuteNonQueryAsync( this MySqlDataSource database, string query, IEnumerable<MySqlParameter> parameters, CancellationToken cancellation = default )
            => database.ExecuteNonQueryAsync(query, command => command.Parameters.AddRange(parameters), cancellation);
        
        public static async Task<int> ExecuteNonQueryAsync( this MySqlDataSource database, string query, IEnumerable<IEnumerable<MySqlParameter>> parameterSet, CancellationToken cancellation = default ) {
            await using ( MySqlConnection connection = await database.OpenConnectionAsync(cancellation) ) {
                return await connection.ExecuteNonQueryAsync(query, parameterSet, cancellation);
            }
        }
        
        #endregion
        #region Connection
        
        public static async Task<int> ExecuteNonQueryAsync( this MySqlConnection connection, string query, Action<MySqlCommand>? func = null, CancellationToken cancellation = default ) {
            await using ( MySqlCommand command = connection.CreateCommand(query, func) ) {
                return await command.ExecuteNonQueryAsync(cancellation);
            }
        }
        
        public static Task<int> ExecuteNonQueryAsync( this MySqlConnection connection, string query, IEnumerable<MySqlParameter> parameters, CancellationToken cancellation = default )
            => connection.ExecuteNonQueryAsync(query, command => command.Parameters.AddRange(parameters), cancellation);
        
        public static async Task<int> ExecuteNonQueryAsync( this MySqlConnection connection, string query, IEnumerable<IEnumerable<MySqlParameter>> parameterSet, CancellationToken cancellation = default ) {
            await using ( MySqlCommand command = connection.CreateCommand(query) ) {
                return await command.ExecuteNonQueryAsync(parameterSet, cancellation);
            }
        }
        
        #endregion
        #region Transaction
        
        public static Task<int> ExecuteNonQueryAsync( this MySqlTransaction transaction, string query, Action<MySqlCommand>? func = null, CancellationToken cancellation = default )
            => Command.Create(query, func)
                .WithTransaction(transaction)
                .ExecuteNonQueryAsync(cancellation);
        
        public static Task<int> ExecuteNonQueryAsync( this MySqlTransaction transaction, string query, IEnumerable<MySqlParameter> parameters, CancellationToken cancellation = default )
            => transaction.ExecuteNonQueryAsync(query, command => command.Parameters.AddRange(parameters), cancellation);
        
        public static Task<int> ExecuteNonQueryAsync( this MySqlTransaction transaction, string query, IEnumerable<IEnumerable<MySqlParameter>> parameterSet, CancellationToken cancellation = default )
            => Command.Create(query)
                .WithTransaction(transaction)
                .ExecuteNonQueryAsync(parameterSet, cancellation);
        
        #endregion
    }
}
