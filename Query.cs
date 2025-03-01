using MySqlConnector;

namespace TheElm.MySql {
    public static class Query {
        #region NonQueries
        
        public static async Task<int> ExecuteNonQueryAsync( this MySqlDataSource database, string query, Action<MySqlCommand>? func = null, CancellationToken cancellation = default ) {
            await using ( MySqlConnection connection = await database.OpenConnectionAsync(cancellation) ) {
                return await connection.ExecuteNonQueryAsync(query, func, cancellation);
            }
        }
        
        public static Task<int> ExecuteNonQueryAsync( this MySqlDataSource database, string query, IEnumerable<MySqlParameter> parameters, CancellationToken cancellation = default )
            => database.ExecuteNonQueryAsync(query, command => command.Parameters.AddRange(parameters), cancellation);
        
        public static Task<int> ExecuteNonQueryAsync( this MySqlConnection connection, string query, Action<MySqlCommand>? func = null, CancellationToken cancellation = default )
            => connection.CreateCommand(query, func).ExecuteNonQueryAsync(cancellation);
        
        public static Task<int> ExecuteNonQueryAsync( this MySqlConnection connection, string query, IEnumerable<MySqlParameter> parameters, CancellationToken cancellation = default )
            => connection.ExecuteNonQueryAsync(query, command => command.Parameters.AddRange(parameters), cancellation);
        
        public static Task<int> ExecuteNonQueryAsync( this MySqlTransaction transaction, string query, Action<MySqlCommand>? func = null, CancellationToken cancellation = default )
            => Command.Create(query, func)
                .WithTransaction(transaction)
                .ExecuteNonQueryAsync(cancellation);
        
        public static Task<int> ExecuteNonQueryAsync( this MySqlTransaction transaction, string query, IEnumerable<MySqlParameter> parameters, CancellationToken cancellation = default )
            => transaction.ExecuteNonQueryAsync(query, command => command.Parameters.AddRange(parameters), cancellation);
        
        #endregion
        
        /// <summary>
        /// Escape the "LIKE" statement wildcards so users can't add their own "%STARTS-WITH" or "ENDS-WITH%"
        /// </summary>
        /// <returns></returns>
        public static MySqlParameter EscapeLike( this MySqlParameter parameter ) {
            if ( parameter is {Value: string strValue} ) {
                parameter.Value = strValue.Replace("%", "\\%")
                    .Replace("_", "\\_");
            }
            
            return parameter;
        }
    }
}
