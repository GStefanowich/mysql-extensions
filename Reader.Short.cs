using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        [return: NotNullIfNotNull(nameof(fallback))]
        public static short? GetNullableInt16( this MySqlDataReader reader, string column, short? fallback = null )
            => reader.TryGetInt16(column, out short value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static short? GetNullableInt16( this MySqlDataReader reader, string table, string column, short? fallback = null )
            => reader.TryGetInt16(table, column, out short value) ? value : fallback;
        
        public static bool TryGetInt16( this MySqlDataReader reader, string column, out short value )
            => reader.TryGetInt16(reader.GetOrdinal(column, null), out value);
        
        public static bool TryGetInt16( this MySqlDataReader reader, string table, string column, out short value )
            => reader.TryGetInt16(reader.GetOrdinal(column, table), out value);
        
        public static bool TryGetInt16( this MySqlDataReader reader, int ordinal, out short value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetInt16(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static ushort? GetNullableUInt16( this MySqlDataReader reader, string column, ushort? fallback = null )
            => reader.TryGetUInt16(column, out ushort value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static ushort? GetNullableUInt16( this MySqlDataReader reader, string table, string column, ushort? fallback = null )
            => reader.TryGetUInt16(table, column, out ushort value) ? value : fallback;
        
        public static bool TryGetUInt16( this MySqlDataReader reader, string column, out ushort value )
            => reader.TryGetUInt16(reader.GetOrdinal(column, null), out value);
        
        public static bool TryGetUInt16( this MySqlDataReader reader, string table, string column, out ushort value )
            => reader.TryGetUInt16(reader.GetOrdinal(column, table), out value);
        
        public static bool TryGetUInt16( this MySqlDataReader reader, int ordinal, out ushort value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetUInt16(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
