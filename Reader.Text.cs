using System.Diagnostics.CodeAnalysis;
using System.IO;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static TextReader GetTextReader( this MySqlDataReader reader, string table, string column )
            => reader.GetTextReader(reader.GetOrdinal(table, column));
        
        public static TextReader? GetNullableTextReader( this MySqlDataReader reader, string column )
            => reader.TryGetTextReader(column, out TextReader? value) ? value : null;
        
        public static TextReader? GetNullableTextReader( this MySqlDataReader reader, string table, string column )
            => reader.TryGetTextReader(table, column, out TextReader? value) ? value : null;
        
        public static bool TryGetTextReader( this MySqlDataReader reader, string column, [NotNullWhen(true)] out TextReader? value )
            => reader.TryGetTextReader(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetTextReader( this MySqlDataReader reader, string table, string column, [NotNullWhen(true)] out TextReader? value )
            => reader.TryGetTextReader(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetTextReader( this MySqlDataReader reader, int ordinal, [NotNullWhen(true)] out TextReader? value ) {
            if ( reader.TryGetString(ordinal, out string? str) ) {
                value = new StringReader(str);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
