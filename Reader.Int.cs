using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        [return: NotNullIfNotNull(nameof(fallback))]
        public static int? GetNullableInt32( this MySqlDataReader reader, string key, int? fallback = null )
            => reader.TryGetInt32(key, out int value) ? value : fallback;
        
        public static bool TryGetInt32( this MySqlDataReader reader, string key, out int value )
            => reader.TryGetInt32(reader.GetOrdinal(key), out value);
        
        public static bool TryGetInt32( this MySqlDataReader reader, int ordinal, out int value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetInt32(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static uint? GetNullableUInt32( this MySqlDataReader reader, string key, uint? fallback = null )
            => reader.TryGetUInt32(key, out uint value) ? value : fallback;
        
        public static bool TryGetUInt32( this MySqlDataReader reader, string key, out uint value )
            => reader.TryGetUInt32(reader.GetOrdinal(key), out value);
        
        public static bool TryGetUInt32( this MySqlDataReader reader, int ordinal, out uint value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetUInt32(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
