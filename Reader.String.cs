using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static string? GetNullableString( this MySqlDataReader reader, string column )
            => reader.TryGetString(column, out string? value) ? value : null;
        
        public static string? GetNullableString( this MySqlDataReader reader, string table, string column )
            => reader.TryGetString(table, column, out string? value) ? value : null;
        
        public static bool TryGetString( this MySqlDataReader reader, string column, [NotNullWhen(true)] out string? value )
            => reader.TryGetString(reader.GetOrdinal(column, null), out value);
        
        public static bool TryGetString( this MySqlDataReader reader, string table, string column, [NotNullWhen(true)] out string? value )
            => reader.TryGetString(reader.GetOrdinal(column, table), out value);
        
        public static bool TryGetString( this MySqlDataReader reader, int ordinal, [NotNullWhen(true)] out string? value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetString(ordinal);
                return true;
            }
            
            value = null;
            return false;
        }
    }
}
