using System.Collections.Generic;
using MySqlConnector;

namespace TheElm.MySql {
    internal static class Extras {
        public static void AddRange( this MySqlParameterCollection collection, IEnumerable<MySqlParameter> parameters ) {
            foreach (MySqlParameter parameter in parameters) {
                collection.Add(parameter);
            }
        }
    }
}
