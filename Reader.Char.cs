using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static char GetChar( this MySqlDataReader reader, string table, string column )
            => reader.GetChar(reader.GetOrdinal(table, column));
        
        public static long GetChars( this MySqlDataReader reader, string table, string column, long dataOffset, char[]? buffer, int bufferOffset, int length )
            => reader.GetChars(reader.GetOrdinal(table, column), dataOffset, buffer, bufferOffset, length);
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static char? GetNullableChar( this MySqlDataReader reader, string column, char? fallback = null )
            => reader.TryGetChar(column, out char value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static char? GetNullableChar( this MySqlDataReader reader, string table, string column, char? fallback = null )
            => reader.TryGetChar(table, column, out char value) ? value : fallback;
        
        public static bool TryGetChar( this MySqlDataReader reader, string column, out char value )
            => reader.TryGetChar(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetChar( this MySqlDataReader reader, string table, string column, out char value )
            => reader.TryGetChar(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetChar( this MySqlDataReader reader, int ordinal, out char value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetChar(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
