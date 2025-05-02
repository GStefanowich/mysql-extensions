using System;
using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static TimeOnly GetTimeOnly( this MySqlDataReader reader, string table, string column )
            => reader.GetTimeOnly(reader.GetOrdinal(table, column));
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static TimeOnly? GetNullableTimeOnly( this MySqlDataReader reader, string column, TimeOnly? fallback = null )
            => reader.TryGetTimeOnly(column, out TimeOnly value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static TimeOnly? GetNullableTimeOnly( this MySqlDataReader reader, string table, string column, TimeOnly? fallback = null )
            => reader.TryGetTimeOnly(table, column, out TimeOnly value) ? value : fallback;
        
        public static bool TryGetTimeOnly( this MySqlDataReader reader, string column, out TimeOnly value )
            => reader.TryGetTimeOnly(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetTimeOnly( this MySqlDataReader reader, string table, string column, out TimeOnly value )
            => reader.TryGetTimeOnly(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetTimeOnly( this MySqlDataReader reader, int ordinal, out TimeOnly value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetTimeOnly(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        public static TimeSpan? GetTimeSpan( this MySqlDataReader reader, string table, string column )
            => reader.GetTimeSpan(reader.GetOrdinal(table, column));
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static TimeSpan? GetNullableTimeSpan( this MySqlDataReader reader, string column, TimeSpan? fallback = null )
            => reader.TryGetTimeSpan(column, out TimeSpan value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static TimeSpan? GetNullableTimeSpan( this MySqlDataReader reader, string table, string column, TimeSpan? fallback = null )
            => reader.TryGetTimeSpan(table, column, out TimeSpan value) ? value : fallback;
        
        public static bool TryGetTimeSpan( this MySqlDataReader reader, string column, out TimeSpan value )
            => reader.TryGetTimeSpan(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetTimeSpan( this MySqlDataReader reader, string table, string column, out TimeSpan value )
            => reader.TryGetTimeSpan(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetTimeSpan( this MySqlDataReader reader, int ordinal, out TimeSpan value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetTimeSpan(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
