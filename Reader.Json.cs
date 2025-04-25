using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MySqlConnector;

namespace TheElm.MySql {
    public static partial class Reader {
        public static T? GetJson<T>( this MySqlDataReader reader, string key, JsonSerializerOptions? options = null )
            => JsonSerializer.Deserialize<T>(reader.GetStream(key), options);
        
        public static ValueTask<T?> GetJsonAsync<T>( this MySqlDataReader reader, string key, JsonSerializerOptions? options = null, CancellationToken cancellation = default )
            => JsonSerializer.DeserializeAsync<T>(reader.GetStream(key), options, cancellation);
        
        public static IAsyncEnumerable<T?> GetJsonEnumerableAsync<T>( this MySqlDataReader reader, string key, JsonSerializerOptions? options = null, CancellationToken cancellation = default )
            => JsonSerializer.DeserializeAsyncEnumerable<T>(reader.GetStream(key), options, cancellation);
    }
}
