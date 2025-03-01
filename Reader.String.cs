using System.Diagnostics.CodeAnalysis;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        [return: NotNullIfNotNull(nameof(fallback))]
        public static string? GetNullableString( this MySqlDataReader reader, string key, string? fallback = null )
            => reader.TryGetString(key, out string? value) ? value : fallback;
        
        public static bool TryGetString( this MySqlDataReader reader, string key, [NotNullWhen(true)] out string? value )
            => reader.TryGetString(reader.GetOrdinal(key), out value);
        
        public static bool TryGetString( this MySqlDataReader reader, int ordinal, [NotNullWhen(true)] out string? value ) {
            if ( !reader.IsDBNull(ordinal) ) {
                value = reader.GetString(ordinal);
                return true;
            }
            
            value = null;
            return false;
        }
    }
}
