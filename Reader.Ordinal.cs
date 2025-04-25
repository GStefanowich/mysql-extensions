using System;
using System.Data.Common;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        /// <summary>
        /// Get the Ordinal for a Column name
        /// </summary>
        /// <param name="reader">MySql reader</param>
        /// <param name="column">Column name to read the ordinal for</param>
        /// <param name="table">Table name to read the ordinal for</param>
        /// <returns>Ordinal value, -1 if not found</returns>
        public static int GetOrdinal( this MySqlDataReader reader, string column, string? table ) {
            foreach ( DbColumn dbColumn in reader.GetColumnSchema() ) {
                if (
                    dbColumn.ColumnOrdinal is int ordinal
                    && string.Equals(dbColumn.ColumnName, column, StringComparison.Ordinal)
                    && (table is null || string.Equals(dbColumn.BaseTableName, table, StringComparison.Ordinal))
                ) {
                    return ordinal;
                }
            }
            
            return -1;
        }
        
        /// <summary>
        /// Try Getting the Ordinal for a column
        /// </summary>
        /// <param name="reader">MySql reader</param>
        /// <param name="column">Column name to read the ordinal for</param>
        /// <param name="ordinal">Ordinal out value</param>
        /// <returns>If the ordinal was found in the reader</returns>
        public static bool TryGetOrdinal( this MySqlDataReader reader, string column, out int ordinal ) {
            ordinal = reader.GetOrdinal(column, table: null);
            return ordinal >= 0 && ordinal < reader.FieldCount;
        }
        
        /// <summary>
        /// Try Getting the Ordinal for a column
        /// </summary>
        /// <param name="reader">MySql reader</param>
        /// <param name="table">Table name to read the ordinal for</param>
        /// <param name="column">Column name to read the ordinal for</param>
        /// <param name="ordinal">Ordinal out value</param>
        /// <returns>If the ordinal was found in the reader</returns>
        public static bool TryGetOrdinal( this MySqlDataReader reader, string table, string column, out int ordinal ) {
            ordinal = reader.GetOrdinal(column, table);
            return ordinal >= 0 && ordinal < reader.FieldCount;
        }
    }
}
