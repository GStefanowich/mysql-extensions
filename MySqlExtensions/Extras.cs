using System.Collections.Generic;
using MySqlConnector;

namespace TheElm.MySql {
    public static class Extras {
        public static void AddRange( this MySqlParameterCollection collection, IEnumerable<MySqlParameter> parameters ) {
            foreach (MySqlParameter parameter in parameters) {
                collection.Add(parameter);
            }
        }
        
        public static void Set( this MySqlParameterCollection collection, IEnumerable<MySqlParameter> parameters ) {
            collection.Clear();
            collection.AddRange(parameters);
        }
    }
}
