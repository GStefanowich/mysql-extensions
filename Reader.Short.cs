using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        [return: NotNullIfNotNull(nameof(fallback))]
        public static short? GetNullableInt16( this MySqlDataReader reader, string key, short? fallback = null )
            => reader.TryGetInt16(key, out short value) ? value : fallback;
        
        public static bool TryGetInt16( this MySqlDataReader reader, string key, out short value )
            => reader.TryGetInt16(reader.GetOrdinal(key), out value);
        
        public static bool TryGetInt16( this MySqlDataReader reader, int ordinal, out short value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetInt16(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static ushort? GetNullableUInt16( this MySqlDataReader reader, string key, ushort? fallback = null )
            => reader.TryGetUInt16(key, out ushort value) ? value : fallback;
        
        public static bool TryGetUInt16( this MySqlDataReader reader, string key, out ushort value )
            => reader.TryGetUInt16(reader.GetOrdinal(key), out value);
        
        public static bool TryGetUInt16( this MySqlDataReader reader, int ordinal, out ushort value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetUInt16(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
