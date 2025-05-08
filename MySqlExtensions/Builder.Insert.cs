using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Builder {
        public static MySqlCommand Insert( this MySqlConnection connection, string table, IEnumerable<IEnumerable<MySqlParameter>> parameterSet )
            => new(Builder.InsertCore(table, parameterSet), connection);
        
        public static MySqlCommand Insert( this MySqlTransaction transaction, string table, IEnumerable<IEnumerable<MySqlParameter>> parameterSet )
            => new(Builder.InsertCore(table, parameterSet), transaction.Connection, transaction);
        
        private static string InsertCore( string table, IEnumerable<IEnumerable<MySqlParameter>> parameterSet ) {
            Dictionary<string, int> columns = new();
            MySqlParameter[] parameters = parameterSet.SelectMany(parameters => parameters.Select(parameter => {
                string name = parameter.ParameterName;
                
                if ( !columns.TryGetValue(name, out int count) ) {
                    count = 1;
                }
                
                columns[name] = count + 1;
                
                // Update the name
                parameter.ParameterName = $"{name}{count}";
                
                return parameter;
            })).ToArray();
            
            if ( parameters.Length % columns.Count != 0 ) {
                throw new Exception("Unequal amount of parameters per row given");
            }
            
            StringBuilder builder = new();
            
            builder.Append("INSERT INTO");
            builder.Append($" `{table}`");
            
            builder.Append(" (");
            builder.Append(string.Join(',', columns.Keys.Select(key => $"`{key}`")));
            builder.Append(") VALUES (");
            
            int i = 1;
            foreach ( MySqlParameter parameter in parameters ) {
                builder.Append($"@{parameter.ParameterName}");
                
                if ( i < parameters.Length ) {
                    if ( i % columns.Count == 0 ) {
                        builder.Append("), (");
                    } else {
                        builder.Append(',');
                    }
                }
                
                i++;
            }
            
            builder.Append(')');
            
            return builder.ToString();
        }
    }
}
