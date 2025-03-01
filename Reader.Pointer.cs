using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        [return: NotNullIfNotNull(nameof(fallback))]
        public static float? GetNullableFloat( this MySqlDataReader reader, string key, float? fallback = null )
            => reader.TryGetFloat(key, out float value) ? value : fallback;
        
        public static bool TryGetFloat( this MySqlDataReader reader, string key, out float value )
            => reader.TryGetFloat(reader.GetOrdinal(key), out value);
        
        public static bool TryGetFloat( this MySqlDataReader reader, int ordinal, out float value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetFloat(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static double? GetNullableDouble( this MySqlDataReader reader, string key, double? fallback = null )
            => reader.TryGetDouble(key, out double value) ? value : fallback;
        
        public static bool TryGetDouble( this MySqlDataReader reader, string key, out double value )
            => reader.TryGetDouble(reader.GetOrdinal(key), out value);
        
        public static bool TryGetDouble( this MySqlDataReader reader, int ordinal, out double value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetDouble(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
