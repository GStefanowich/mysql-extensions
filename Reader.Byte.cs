using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        [return: NotNullIfNotNull(nameof(fallback))]
        public static byte? GetNullableByte( this MySqlDataReader reader, string column, byte? fallback = null )
            => reader.TryGetByte(column, out byte value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static byte? GetNullableByte( this MySqlDataReader reader, string table, string column, byte? fallback = null )
            => reader.TryGetByte(table, column, out byte value) ? value : fallback;
        
        public static bool TryGetByte( this MySqlDataReader reader, string column, out byte value )
            => reader.TryGetByte(reader.GetOrdinal(column, null), out value);
        
        public static bool TryGetByte( this MySqlDataReader reader, string table, string column, out byte value )
            => reader.TryGetByte(reader.GetOrdinal(column, table), out value);
        
        public static bool TryGetByte( this MySqlDataReader reader, int ordinal, out byte value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetByte(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static sbyte? GetNullableSByte( this MySqlDataReader reader, string column, sbyte? fallback = null )
            => reader.TryGetSByte(column, out sbyte value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static sbyte? GetNullableSByte( this MySqlDataReader reader, string table, string column, sbyte? fallback = null )
            => reader.TryGetSByte(table, column, out sbyte value) ? value : fallback;
        
        public static bool TryGetSByte( this MySqlDataReader reader, string column, out sbyte value )
            => reader.TryGetSByte(reader.GetOrdinal(column, null), out value);
        
        public static bool TryGetSByte( this MySqlDataReader reader, string table, string column, out sbyte value )
            => reader.TryGetSByte(reader.GetOrdinal(column, table), out value);
        
        public static bool TryGetSByte( this MySqlDataReader reader, int ordinal, out sbyte value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetSByte(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
