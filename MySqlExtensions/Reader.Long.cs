using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static long GetInt64( this MySqlDataReader reader, string table, string column )
            => reader.GetInt64(reader.GetOrdinal(table, column));
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static long? GetNullableInt64( this MySqlDataReader reader, string column, long? fallback = null )
            => reader.TryGetInt64(column, out long value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static long? GetNullableInt64( this MySqlDataReader reader, string table, string column, long? fallback = null )
            => reader.TryGetInt64(table, column, out long value) ? value : fallback;
        
        public static bool TryGetInt64( this MySqlDataReader reader, string column, out long value )
            => reader.TryGetInt64(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetInt64( this MySqlDataReader reader, string table, string column, out long value )
            => reader.TryGetInt64(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetInt64( this MySqlDataReader reader, int ordinal, out long value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetInt64(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        public static ulong GetUInt64( this MySqlDataReader reader, string table, string column )
            => reader.GetUInt64(reader.GetOrdinal(table, column));
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static ulong? GetNullableUInt64( this MySqlDataReader reader, string column, ulong? fallback = null )
            => reader.TryGetUInt64(column, out ulong value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static ulong? GetNullableUInt64( this MySqlDataReader reader, string table, string column, ulong? fallback = null )
            => reader.TryGetUInt64(table, column, out ulong value) ? value : fallback;
        
        public static bool TryGetUInt64( this MySqlDataReader reader, string column, out ulong value )
            => reader.TryGetUInt64(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetUInt64( this MySqlDataReader reader, string table, string column, out ulong value )
            => reader.TryGetUInt64(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetUInt64( this MySqlDataReader reader, int ordinal, out ulong value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetUInt64(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
