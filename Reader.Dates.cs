using System;
using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateTimeOffset? GetNullableDateTime( this MySqlDataReader reader, string key, DateTime? fallback = null )
            => reader.TryGetDateTime(key, out DateTime value) ? value : fallback;
        
        public static bool TryGetDateTime( this MySqlDataReader reader, string key, out DateTime value )
            => reader.TryGetDateTime(reader.GetOrdinal(key), out value);
        
        public static bool TryGetDateTime( this MySqlDataReader reader, int ordinal, out DateTime value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetDateTime(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateTimeOffset? GetNullableDateTimeOffset( this MySqlDataReader reader, string key, DateTimeOffset? fallback = null )
            => reader.TryGetDateTimeOffset(key, out DateTimeOffset value) ? value : fallback;
        
        public static bool TryGetDateTimeOffset( this MySqlDataReader reader, string key, out DateTimeOffset value )
            => reader.TryGetDateTimeOffset(reader.GetOrdinal(key), out value);
        
        public static bool TryGetDateTimeOffset( this MySqlDataReader reader, int ordinal, out DateTimeOffset value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetDateTimeOffset(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static DateOnly? GetNullableDateOnly( this MySqlDataReader reader, string key, DateOnly? fallback = null )
            => reader.TryGetDateOnly(key, out DateOnly value) ? value : fallback;
        
        public static bool TryGetDateOnly( this MySqlDataReader reader, string key, out DateOnly value )
            => reader.TryGetDateOnly(reader.GetOrdinal(key), out value);
        
        public static bool TryGetDateOnly( this MySqlDataReader reader, int ordinal, out DateOnly value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetDateOnly(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static MySqlDateTime? GetNullableMySqlDateTime( this MySqlDataReader reader, string key, MySqlDateTime? fallback = null )
            => reader.TryGetMySqlDateTime(key, out MySqlDateTime value) ? value : fallback;
        
        public static bool TryGetMySqlDateTime( this MySqlDataReader reader, string key, out MySqlDateTime value )
            => reader.TryGetMySqlDateTime(reader.GetOrdinal(key), out value);
        
        public static bool TryGetMySqlDateTime( this MySqlDataReader reader, int ordinal, out MySqlDateTime value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetMySqlDateTime(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
