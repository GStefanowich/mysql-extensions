using System;
using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateTimeOffset? GetNullableDateTime( this MySqlDataReader reader, string column, DateTime? fallback = null )
            => reader.TryGetDateTime(column, out DateTime value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateTimeOffset? GetNullableDateTime( this MySqlDataReader reader, string table, string column, DateTime? fallback = null )
            => reader.TryGetDateTime(table, column, out DateTime value) ? value : fallback;
        
        public static bool TryGetDateTime( this MySqlDataReader reader, string column, out DateTime value )
            => reader.TryGetDateTime(reader.GetOrdinal(column, null), out value);
        
        public static bool TryGetDateTime( this MySqlDataReader reader, string table, string column, out DateTime value )
            => reader.TryGetDateTime(reader.GetOrdinal(column, table), out value);
        
        public static bool TryGetDateTime( this MySqlDataReader reader, int ordinal, out DateTime value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetDateTime(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateTimeOffset? GetNullableDateTimeOffset( this MySqlDataReader reader, string column, DateTimeOffset? fallback = null )
            => reader.TryGetDateTimeOffset(column, out DateTimeOffset value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateTimeOffset? GetNullableDateTimeOffset( this MySqlDataReader reader, string table, string column, DateTimeOffset? fallback = null )
            => reader.TryGetDateTimeOffset(table, column, out DateTimeOffset value) ? value : fallback;
        
        public static bool TryGetDateTimeOffset( this MySqlDataReader reader, string column, out DateTimeOffset value )
            => reader.TryGetDateTimeOffset(reader.GetOrdinal(column, null), out value);
        
        public static bool TryGetDateTimeOffset( this MySqlDataReader reader, string table, string column, out DateTimeOffset value )
            => reader.TryGetDateTimeOffset(reader.GetOrdinal(column, table), out value);
        
        public static bool TryGetDateTimeOffset( this MySqlDataReader reader, int ordinal, out DateTimeOffset value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetDateTimeOffset(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateOnly? GetNullableDateOnly( this MySqlDataReader reader, string column, DateOnly? fallback = null )
            => reader.TryGetDateOnly(column, out DateOnly value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateOnly? GetNullableDateOnly( this MySqlDataReader reader, string table, string column, DateOnly? fallback = null )
            => reader.TryGetDateOnly(table, column, out DateOnly value) ? value : fallback;
        
        public static bool TryGetDateOnly( this MySqlDataReader reader, string column, out DateOnly value )
            => reader.TryGetDateOnly(reader.GetOrdinal(column, null), out value);
        
        public static bool TryGetDateOnly( this MySqlDataReader reader, string table, string column, out DateOnly value )
            => reader.TryGetDateOnly(reader.GetOrdinal(column, table), out value);
        
        public static bool TryGetDateOnly( this MySqlDataReader reader, int ordinal, out DateOnly value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetDateOnly(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static MySqlDateTime? GetNullableMySqlDateTime( this MySqlDataReader reader, string column, MySqlDateTime? fallback = null )
            => reader.TryGetMySqlDateTime(column, out MySqlDateTime value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static MySqlDateTime? GetNullableMySqlDateTime( this MySqlDataReader reader, string table, string column, MySqlDateTime? fallback = null )
            => reader.TryGetMySqlDateTime(table, column, out MySqlDateTime value) ? value : fallback;
        
        public static bool TryGetMySqlDateTime( this MySqlDataReader reader, string column, out MySqlDateTime value )
            => reader.TryGetMySqlDateTime(reader.GetOrdinal(column, null), out value);
        
        public static bool TryGetMySqlDateTime( this MySqlDataReader reader, string table, string column, out MySqlDateTime value )
            => reader.TryGetMySqlDateTime(reader.GetOrdinal(column, table), out value);
        
        public static bool TryGetMySqlDateTime( this MySqlDataReader reader, int ordinal, out MySqlDateTime value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetMySqlDateTime(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
