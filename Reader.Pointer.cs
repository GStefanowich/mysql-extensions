using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static float GetFloat( this MySqlDataReader reader, string table, string column )
            => reader.GetFloat(reader.GetOrdinal(table, column));
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static float? GetNullableFloat( this MySqlDataReader reader, string column, float? fallback = null )
            => reader.TryGetFloat(column, out float value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static float? GetNullableFloat( this MySqlDataReader reader, string table, string column, float? fallback = null )
            => reader.TryGetFloat(table, column, out float value) ? value : fallback;
        
        public static bool TryGetFloat( this MySqlDataReader reader, string column, out float value )
            => reader.TryGetFloat(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetFloat( this MySqlDataReader reader, string table, string column, out float value )
            => reader.TryGetFloat(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetFloat( this MySqlDataReader reader, int ordinal, out float value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetFloat(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        public static double GetDouble( this MySqlDataReader reader, string table, string column )
            => reader.GetDouble(reader.GetOrdinal(table, column));
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static double? GetNullableDouble( this MySqlDataReader reader, string column, double? fallback = null )
            => reader.TryGetDouble(column, out double value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static double? GetNullableDouble( this MySqlDataReader reader, string table, string column, double? fallback = null )
            => reader.TryGetDouble(table, column, out double value) ? value : fallback;
        
        public static bool TryGetDouble( this MySqlDataReader reader, string column, out double value )
            => reader.TryGetDouble(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetDouble( this MySqlDataReader reader, string table, string column, out double value )
            => reader.TryGetDouble(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetDouble( this MySqlDataReader reader, int ordinal, out double value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetDouble(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
