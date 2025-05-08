using System;
using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static Guid GetGuid( this MySqlDataReader reader, string table, string column )
            => reader.GetGuid(reader.GetOrdinal(table, column));
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static Guid? GetNullableGuid( this MySqlDataReader reader, string column, Guid? fallback = null )
            => reader.TryGetGuid(column, out Guid value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static Guid? GetNullableGuid( this MySqlDataReader reader, string table, string column, Guid? fallback = null )
            => reader.TryGetGuid(table, column, out Guid value) ? value : fallback;
        
        public static bool TryGetGuid( this MySqlDataReader reader, string column, out Guid value )
            => reader.TryGetGuid(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetGuid( this MySqlDataReader reader, string table, string column, out Guid value )
            => reader.TryGetGuid(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetGuid( this MySqlDataReader reader, int ordinal, out Guid value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetGuid(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
