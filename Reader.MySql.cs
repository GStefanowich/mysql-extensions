using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        #region DateTime
        
        public static MySqlDateTime GetMySqlDateTime( this MySqlDataReader reader, string table, string column )
            => reader.GetMySqlDateTime(reader.GetOrdinal(table, column));
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static MySqlDateTime? GetNullableMySqlDateTime( this MySqlDataReader reader, string column, MySqlDateTime? fallback = null )
            => reader.TryGetMySqlDateTime(column, out MySqlDateTime value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static MySqlDateTime? GetNullableMySqlDateTime( this MySqlDataReader reader, string table, string column, MySqlDateTime? fallback = null )
            => reader.TryGetMySqlDateTime(table, column, out MySqlDateTime value) ? value : fallback;
        
        public static bool TryGetMySqlDateTime( this MySqlDataReader reader, string column, out MySqlDateTime value )
            => reader.TryGetMySqlDateTime(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetMySqlDateTime( this MySqlDataReader reader, string table, string column, out MySqlDateTime value )
            => reader.TryGetMySqlDateTime(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetMySqlDateTime( this MySqlDataReader reader, int ordinal, out MySqlDateTime value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetMySqlDateTime(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        #endregion
        #region Decimal
        
        public static MySqlDecimal GetMySqlDecimal( this MySqlDataReader reader, string table, string column )
            => reader.GetMySqlDecimal(reader.GetOrdinal(table, column));
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static MySqlDecimal? GetNullableMySqlDecimal( this MySqlDataReader reader, string column, MySqlDecimal? fallback = null )
            => reader.TryGetMySqlDecimal(column, out MySqlDecimal value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static MySqlDecimal? GetNullableMySqlDecimal( this MySqlDataReader reader, string table, string column, MySqlDecimal? fallback = null )
            => reader.TryGetMySqlDecimal(table, column, out MySqlDecimal value) ? value : fallback;
        
        public static bool TryGetMySqlDecimal( this MySqlDataReader reader, string column, out MySqlDecimal value )
            => reader.TryGetMySqlDecimal(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetMySqlDecimal( this MySqlDataReader reader, string table, string column, out MySqlDecimal value )
            => reader.TryGetMySqlDecimal(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetMySqlDecimal( this MySqlDataReader reader, int ordinal, out MySqlDecimal value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetMySqlDecimal(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        #endregion
        #region Geometry
        
        public static MySqlGeometry GetMySqlGeometry( this MySqlDataReader reader, string table, string column )
            => reader.GetMySqlGeometry(reader.GetOrdinal(table, column));
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static MySqlGeometry? GetNullableMySqlDateTime( this MySqlDataReader reader, string column, MySqlGeometry? fallback = null )
            => reader.TryGetMySqlGeometry(column, out MySqlGeometry? value) ? value : fallback;
        
        [return: NotNullIfNotNull(nameof(fallback))]
        public static MySqlGeometry? GetNullableMySqlDateTime( this MySqlDataReader reader, string table, string column, MySqlGeometry? fallback = null )
            => reader.TryGetMySqlGeometry(table, column, out MySqlGeometry? value) ? value : fallback;
        
        public static bool TryGetMySqlGeometry( this MySqlDataReader reader, string column, [NotNullWhen(true)] out MySqlGeometry? value )
            => reader.TryGetMySqlGeometry(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetMySqlGeometry( this MySqlDataReader reader, string table, string column, [NotNullWhen(true)] out MySqlGeometry? value )
            => reader.TryGetMySqlGeometry(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetMySqlGeometry( this MySqlDataReader reader, int ordinal, [NotNullWhen(true)] out MySqlGeometry? value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetMySqlGeometry(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
        
        #endregion
    }
}
