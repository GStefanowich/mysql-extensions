using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static bool GetBoolean( this MySqlDataReader reader, string table, string column )
            => reader.GetBoolean(reader.GetOrdinal(table, column));
        
        public static bool GetBoolean( this MySqlDataReader reader, string table, string column, bool defaultValue )
            => reader.TryGetOrdinal(table, column, out int ordinal) && !reader.IsDBNull(ordinal) ? reader.GetBoolean(ordinal) : defaultValue;
        
        public static bool GetBoolean( this MySqlDataReader reader, string column )
            => reader.GetBoolean(reader.GetOrdinal(null, column));
        
        public static bool GetBoolean( this MySqlDataReader reader, string column, bool defaultValue )
            => reader.TryGetOrdinal(column, out int ordinal) && !reader.IsDBNull(ordinal) ? reader.GetBoolean(ordinal) : defaultValue;
    }
}
