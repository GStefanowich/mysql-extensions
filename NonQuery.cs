using System;
using System.Collections.Generic;
using System.Linq;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class NonQuery {
        #region Command
        
        public static int ExecuteNonQuery( this MySqlCommand command, IEnumerable<MySqlParameter> parameters ) {
            command.Parameters.Clear();
            command.Parameters.AddRange(parameters);
            
            return command.ExecuteNonQuery();
        }
        
        public static int ExecuteNonQuery( this MySqlCommand command, IEnumerable<IEnumerable<MySqlParameter>> parameterSet )
            => parameterSet.Sum(parameters => command.ExecuteNonQuery(parameters));
        
        #endregion
        #region Source
        
        public static int ExecuteNonQuery( this MySqlDataSource database, string query, Action<MySqlCommand>? func = null ) {
            using ( MySqlConnection connection = database.OpenConnection() ) {
                return connection.ExecuteNonQuery(query, func);
            }
        }
        
        public static int ExecuteNonQuery( this MySqlDataSource database, string query, IEnumerable<MySqlParameter> parameters )
            => database.ExecuteNonQuery(query, command => command.Parameters.AddRange(parameters));
        
        public static int ExecuteNonQuery( this MySqlDataSource database, string query, IEnumerable<IEnumerable<MySqlParameter>> parameterSet ) {
            using ( MySqlConnection connection = database.OpenConnection() ) {
                return connection.ExecuteNonQuery(query, parameterSet);
            }
        }
        
        #endregion
        #region Connection
        
        public static int ExecuteNonQuery( this MySqlConnection connection, string query, Action<MySqlCommand>? func = null ) {
            using ( MySqlCommand command = connection.CreateCommand(query, func) ) {
                return command.ExecuteNonQuery();
            }
        }
        
        public static int ExecuteNonQuery( this MySqlConnection connection, string query, IEnumerable<MySqlParameter> parameters )
            => connection.ExecuteNonQuery(query, command => command.Parameters.AddRange(parameters));
        
        public static int ExecuteNonQuery( this MySqlConnection connection, string query, IEnumerable<IEnumerable<MySqlParameter>> parameterSet ) {
            using ( MySqlCommand command = connection.CreateCommand(query) ) {
                return command.ExecuteNonQuery(parameterSet);
            }
        }
        
        #endregion
        #region Transaction
        
        public static int ExecuteNonQuery( this MySqlTransaction transaction, string query, Action<MySqlCommand>? func = null )
            => Command.Create(query, func)
                .WithTransaction(transaction)
                .ExecuteNonQuery();
        
        public static int ExecuteNonQuery( this MySqlTransaction transaction, string query, IEnumerable<MySqlParameter> parameters )
            => transaction.ExecuteNonQuery(query, command => command.Parameters.AddRange(parameters));
        
        public static int ExecuteNonQuery( this MySqlTransaction transaction, string query, IEnumerable<IEnumerable<MySqlParameter>> parameterSet )
            => Command.Create(query)
                .WithTransaction(transaction)
                .ExecuteNonQuery(parameterSet);
        
        #endregion
    }
}
