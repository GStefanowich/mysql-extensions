using System;
using System.Collections.Generic;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class NonQuery {
        public static int ExecuteNonQuery( this MySqlDataSource database, string query, Action<MySqlCommand>? func = null ) {
            using ( MySqlConnection connection = database.OpenConnection() ) {
                return connection.ExecuteNonQuery(query, func);
            }
        }
        
        public static int ExecuteNonQuery( this MySqlDataSource database, string query, IEnumerable<MySqlParameter> parameters )
            => database.ExecuteNonQuery(query, command => command.Parameters.AddRange(parameters));
        
        public static int ExecuteNonQuery( this MySqlConnection connection, string query, Action<MySqlCommand>? func = null )
            => connection.CreateCommand(query, func).ExecuteNonQuery();
        
        public static int ExecuteNonQuery( this MySqlConnection connection, string query, IEnumerable<MySqlParameter> parameters )
            => connection.ExecuteNonQuery(query, command => command.Parameters.AddRange(parameters));
        
        public static int ExecuteNonQuery( this MySqlTransaction transaction, string query, Action<MySqlCommand>? func = null )
            => Command.Create(query, func)
                .WithTransaction(transaction)
                .ExecuteNonQuery();
        
        public static int ExecuteNonQuery( this MySqlTransaction transaction, string query, IEnumerable<MySqlParameter> parameters )
            => transaction.ExecuteNonQuery(query, command => command.Parameters.AddRange(parameters));
    }
}
