using System;
using System.Collections.Generic;
using MySqlConnector;

namespace TheElm.MySql {
    public static class Command {
        public static MySqlCommand Create( string query, IEnumerable<MySqlParameter> parameters )
            => Command.Create(query, command => command.Parameters.AddRange(parameters));
        
        public static MySqlCommand Create( string query, Action<MySqlCommand>? func = null )
            => new MySqlCommand(query).Invoke(func);
        
        public static MySqlCommand CreateCommand( this MySqlConnection connection, string query, Action<MySqlCommand>? func = null )
            => Command.Create(query, func)
                .WithConnection(connection);
        
        public static MySqlCommand CreateCommand( this MySqlConnection connection, string query, IEnumerable<MySqlParameter> parameters )
            => connection.CreateCommand(query, command => command.Parameters.AddRange(parameters));
        
        public static MySqlCommand WithTransaction( this MySqlCommand command, MySqlTransaction transaction ) {
            command.Transaction = transaction;
            command.Connection = transaction.Connection;
            
            return command;
        }
        
        public static MySqlCommand WithConnection( this MySqlCommand command, MySqlConnection connection ) {
            command.Transaction = null;
            command.Connection = connection;
            return command;
        }
        
        private static MySqlCommand Invoke( this MySqlCommand command, Action<MySqlCommand>? func ) {
            func?.Invoke(command);
            return command;
        }
    }
}
