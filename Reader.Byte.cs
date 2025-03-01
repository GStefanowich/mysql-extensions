using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        [return: NotNullIfNotNull(nameof(fallback))]
        public static byte? GetNullableByte( this MySqlDataReader reader, string key, byte? fallback = null )
            => reader.TryGetByte(key, out byte value) ? value : fallback;
        
        public static bool TryGetByte( this MySqlDataReader reader, string key, out byte value )
            => reader.TryGetByte(reader.GetOrdinal(key), out value);
        
        public static bool TryGetByte( this MySqlDataReader reader, int ordinal, out byte value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetByte(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static sbyte? GetNullableSByte( this MySqlDataReader reader, string key, sbyte? fallback = null )
            => reader.TryGetSByte(key, out sbyte value) ? value : fallback;
        
        public static bool TryGetSByte( this MySqlDataReader reader, string key, out sbyte value )
            => reader.TryGetSByte(reader.GetOrdinal(key), out value);
        
        public static bool TryGetSByte( this MySqlDataReader reader, int ordinal, out sbyte value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetSByte(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
