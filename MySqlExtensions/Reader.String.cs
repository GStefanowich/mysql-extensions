using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static string GetString( this MySqlDataReader reader, string table, string column )
            => reader.GetString(reader.GetOrdinal(table, column));
        
        public static string? GetNullableString( this MySqlDataReader reader, string column )
            => reader.TryGetString(column, out string? value) ? value : null;
        
        public static string? GetNullableString( this MySqlDataReader reader, string table, string column )
            => reader.TryGetString(table, column, out string? value) ? value : null;
        
        public static bool TryGetString( this MySqlDataReader reader, string column, [NotNullWhen(true)] out string? value )
            => reader.TryGetString(reader.GetOrdinal(null, column), out value);
        
        public static bool TryGetString( this MySqlDataReader reader, string table, string column, [NotNullWhen(true)] out string? value )
            => reader.TryGetString(reader.GetOrdinal(table, column), out value);
        
        public static bool TryGetString( this MySqlDataReader reader, int ordinal, [NotNullWhen(true)] out string? value ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetString(ordinal);
                return true;
            }
            
            value = null;
            return false;
        }
        
        public static string GetBinaryString( this MySqlDataReader reader, int ordinal, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true ) {
            using ( StreamReader stream = new(reader.GetStream(ordinal), encoding ?? Encoding.UTF8, detectEncodingFromByteOrderMarks) ) {
                return stream.ReadToEnd();
            }
        }
        
        public static string GetBinaryString( this MySqlDataReader reader, string column, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true )
            => reader.GetBinaryString(reader.GetOrdinal(null, column), encoding, detectEncodingFromByteOrderMarks);
        
        public static string GetBinaryString( this MySqlDataReader reader, string table, string column, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true )
            => reader.GetBinaryString(reader.GetOrdinal(table, column), encoding, detectEncodingFromByteOrderMarks);
        
        public static string? GetNullableBinaryString( this MySqlDataReader reader, string column, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true )
            => reader.TryGetBinaryString(column, out string? value, encoding, detectEncodingFromByteOrderMarks) ? value : null;
        
        public static string? GetNullableBinaryString( this MySqlDataReader reader, string table, string column, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true )
            => reader.TryGetBinaryString(table, column, out string? value, encoding, detectEncodingFromByteOrderMarks) ? value : null;
        
        public static bool TryGetBinaryString( this MySqlDataReader reader, string column, [NotNullWhen(true)] out string? value, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true )
            => reader.TryGetBinaryString(reader.GetOrdinal(null, column), out value, encoding, detectEncodingFromByteOrderMarks);
        
        public static bool TryGetBinaryString( this MySqlDataReader reader, string table, string column, [NotNullWhen(true)] out string? value, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true )
            => reader.TryGetBinaryString(reader.GetOrdinal(table, column), out value, encoding, detectEncodingFromByteOrderMarks);
        
        public static bool TryGetBinaryString( this MySqlDataReader reader, int ordinal, [NotNullWhen(true)] out string? value, Encoding? encoding = null, bool detectEncodingFromByteOrderMarks = true ) {
            if ( ordinal >= 0 && !reader.IsDBNull(ordinal) ) {
                value = reader.GetBinaryString(ordinal, encoding, detectEncodingFromByteOrderMarks);
                return true;
            }
            
            value = null;
            return false;
        }
    }
}
