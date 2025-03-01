using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        [return: NotNullIfNotNull(nameof(fallback))]
        public static long? GetNullableInt64( this MySqlDataReader reader, string key, long? fallback = null )
            => reader.TryGetInt64(key, out long value) ? value : fallback;
        
        public static bool TryGetInt64( this MySqlDataReader reader, string key, out long value )
            => reader.TryGetInt64(reader.GetOrdinal(key), out value);
        
        public static bool TryGetInt64( this MySqlDataReader reader, int ordinal, out long value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetInt64(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static ulong? GetNullableUInt64( this MySqlDataReader reader, string key, ulong? fallback = null )
            => reader.TryGetUInt64(key, out ulong value) ? value : fallback;
        
        public static bool TryGetUInt64( this MySqlDataReader reader, string key, out ulong value )
            => reader.TryGetUInt64(reader.GetOrdinal(key), out value);
        
        public static bool TryGetUInt64( this MySqlDataReader reader, int ordinal, out ulong value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetUInt64(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
