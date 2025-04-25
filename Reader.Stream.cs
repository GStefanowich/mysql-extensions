using System.IO;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static Stream GetStream( this MySqlDataReader reader, string table, string column )
            => reader.GetStream(reader.GetOrdinal(column, table));
    }
}
