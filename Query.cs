using MySqlConnector;

namespace TheElm.MySql {
    public static class Query {
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
