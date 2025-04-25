using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        [return: NotNullIfNotNull(nameof(fallback))]
        public static int? GetNullableInt32( this MySqlDataReader reader, string column, int? fallback = null )
            => reader.TryGetInt32(column, out int value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static int? GetNullableInt32( this MySqlDataReader reader, string table, string column, int? fallback = null )
            => reader.TryGetInt32(table, column, out int value) ? value : fallback;
        
        public static bool TryGetInt32( this MySqlDataReader reader, string column, out int value )
            => reader.TryGetInt32(reader.GetOrdinal(column, null), out value);
        
        public static bool TryGetInt32( this MySqlDataReader reader, string table, string column, out int value )
            => reader.TryGetInt32(reader.GetOrdinal(column, table), out value);
        
        public static bool TryGetInt32( this MySqlDataReader reader, int ordinal, out int value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetInt32(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static uint? GetNullableUInt32( this MySqlDataReader reader, string column, uint? fallback = null )
            => reader.TryGetUInt32(column, out uint value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static uint? GetNullableUInt32( this MySqlDataReader reader, string table, string column, uint? fallback = null )
            => reader.TryGetUInt32(table, column, out uint value) ? value : fallback;
        
        public static bool TryGetUInt32( this MySqlDataReader reader, string column, out uint value )
            => reader.TryGetUInt32(reader.GetOrdinal(column, null), out value);
        
        public static bool TryGetUInt32( this MySqlDataReader reader, string table, string column, out uint value )
            => reader.TryGetUInt32(reader.GetOrdinal(column, table), out value);
        
        public static bool TryGetUInt32( this MySqlDataReader reader, int ordinal, out uint value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetUInt32(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
