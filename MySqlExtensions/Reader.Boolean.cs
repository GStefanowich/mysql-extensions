using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static bool GetBoolean( this MySqlDataReader reader, string table, string column )
            => reader.GetBoolean(reader.GetOrdinal(table, column));
    }
}
