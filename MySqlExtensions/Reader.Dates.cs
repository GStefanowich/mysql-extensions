using System;
using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static DateTime GetDateTime( this MySqlDataReader reader, string table, string column )
            => reader.GetDateTime(reader.GetOrdinal(table, column));
        
        public static DateTime GetDateTime( this MySqlDataReader reader, string column, DateTime fallback )
            => reader.TryGetDateTime(column, out DateTime value) ? value : fallback;
        
        public static DateTime GetDateTime( this MySqlDataReader reader, string table, string column, DateTime fallback )
            => reader.TryGetDateTime(table, column, out DateTime value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateTime? GetNullableDateTime( this MySqlDataReader reader, string column, DateTime? fallback = null )
            => reader.TryGetDateTime(column, out DateTime value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateTime? GetNullableDateTime( this MySqlDataReader reader, string table, string column, DateTime? fallback = null )
            => reader.TryGetDateTime(table, column, out DateTime value) ? value : fallback;
        
        public static bool TryGetDateTime( this MySqlDataReader reader, string column, out DateTime value )
            => reader.TryGetDateTime(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetDateTime( this MySqlDataReader reader, string table, string column, out DateTime value )
            => reader.TryGetDateTime(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetDateTime( this MySqlDataReader reader, int ordinal, out DateTime value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetDateTime(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        public static DateTimeOffset GetDateTimeOffset( this MySqlDataReader reader, string table, string column )
            => reader.GetDateTimeOffset(reader.GetOrdinal(table, column));
        
        public static DateTimeOffset GetDateTimeOffset( this MySqlDataReader reader, string column, DateTimeOffset fallback )
            => reader.TryGetDateTimeOffset(column, out DateTimeOffset value) ? value : fallback;
        
        public static DateTimeOffset GetDateTimeOffset( this MySqlDataReader reader, string table, string column, DateTimeOffset fallback )
            => reader.TryGetDateTimeOffset(table, column, out DateTimeOffset value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateTimeOffset? GetNullableDateTimeOffset( this MySqlDataReader reader, string column, DateTimeOffset? fallback = null )
            => reader.TryGetDateTimeOffset(column, out DateTimeOffset value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateTimeOffset? GetNullableDateTimeOffset( this MySqlDataReader reader, string table, string column, DateTimeOffset? fallback = null )
            => reader.TryGetDateTimeOffset(table, column, out DateTimeOffset value) ? value : fallback;
        
        public static bool TryGetDateTimeOffset( this MySqlDataReader reader, string column, out DateTimeOffset value )
            => reader.TryGetDateTimeOffset(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetDateTimeOffset( this MySqlDataReader reader, string table, string column, out DateTimeOffset value )
            => reader.TryGetDateTimeOffset(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetDateTimeOffset( this MySqlDataReader reader, int ordinal, out DateTimeOffset value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetDateTimeOffset(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        public static DateOnly? GetDateOnly( this MySqlDataReader reader, string table, string column )
            => reader.GetDateOnly(reader.GetOrdinal(table, column));
        
        public static DateOnly GetDateOnly( this MySqlDataReader reader, string column, DateOnly fallback )
            => reader.TryGetDateOnly(column, out DateOnly value) ? value : fallback;
        
        public static DateOnly GetDateOnly( this MySqlDataReader reader, string table, string column, DateOnly fallback )
            => reader.TryGetDateOnly(table, column, out DateOnly value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateOnly? GetNullableDateOnly( this MySqlDataReader reader, string column, DateOnly? fallback = null )
            => reader.TryGetDateOnly(column, out DateOnly value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateOnly? GetNullableDateOnly( this MySqlDataReader reader, string table, string column, DateOnly? fallback = null )
            => reader.TryGetDateOnly(table, column, out DateOnly value) ? value : fallback;
        
        public static bool TryGetDateOnly( this MySqlDataReader reader, string column, out DateOnly value )
            => reader.TryGetDateOnly(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetDateOnly( this MySqlDataReader reader, string table, string column, out DateOnly value )
            => reader.TryGetDateOnly(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetDateOnly( this MySqlDataReader reader, int ordinal, out DateOnly value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetDateOnly(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
