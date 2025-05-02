using System;
using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static Type GetFieldType( this MySqlDataReader reader, string table, string column )
            => reader.GetFieldType(reader.GetOrdinal(table, column));
        
        public static Type? GetNullableFieldType( this MySqlDataReader reader, string column )
            => reader.TryGetFieldType(column, out Type? value) ? value : null;
        
        public static Type? GetNullableFieldType( this MySqlDataReader reader, string table, string column )
            => reader.TryGetFieldType(table, column, out Type? value) ? value : null;
        
        public static bool TryGetFieldType( this MySqlDataReader reader, string column, [NotNullWhen(true)] out Type? value )
            => reader.TryGetFieldType(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetFieldType( this MySqlDataReader reader, string table, string column, [NotNullWhen(true)] out Type? value )
            => reader.TryGetFieldType(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetFieldType( this MySqlDataReader reader, int ordinal, [NotNullWhen(true)] out Type? value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetFieldType(ordinal);
                return true;
            }
            
            value = default;
            return false;
        }
    }
}
