using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static byte GetByte( this MySqlDataReader reader, string table, string column )
            => reader.GetByte(reader.GetOrdinal(table, column));
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static byte? GetNullableByte( this MySqlDataReader reader, string column, byte? fallback = null )
            => reader.TryGetByte(column, out byte value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static byte? GetNullableByte( this MySqlDataReader reader, string table, string column, byte? fallback = null )
            => reader.TryGetByte(table, column, out byte value) ? value : fallback;
        
        public static bool TryGetByte( this MySqlDataReader reader, string column, out byte value )
            => reader.TryGetByte(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetByte( this MySqlDataReader reader, string table, string column, out byte value )
            => reader.TryGetByte(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetByte( this MySqlDataReader reader, int ordinal, out byte value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetByte(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        public static sbyte GetSByte( this MySqlDataReader reader, string table, string column )
            => reader.GetSByte(reader.GetOrdinal(table, column));
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static sbyte? GetNullableSByte( this MySqlDataReader reader, string column, sbyte? fallback = null )
            => reader.TryGetSByte(column, out sbyte value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static sbyte? GetNullableSByte( this MySqlDataReader reader, string table, string column, sbyte? fallback = null )
            => reader.TryGetSByte(table, column, out sbyte value) ? value : fallback;
        
        public static bool TryGetSByte( this MySqlDataReader reader, string column, out sbyte value )
            => reader.TryGetSByte(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetSByte( this MySqlDataReader reader, string table, string column, out sbyte value )
            => reader.TryGetSByte(reader.GetOrdinal(table, column), out value);
        
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
