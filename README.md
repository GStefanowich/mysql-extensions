# About

This is an extension library for working with [MySqlConnector](https://mysqlconnector.net/)

It adds a lot of extension methods for dealing with Null checks, as well as implementation with IAsyncEnumerable to use Asynchronously LINQ queries

## Examples

- Insert

```csharp
MySqlDataSource source;

// Asynchronous
await source.ExecuteNonQueryAsync("INSERT INTO `table` (`col`) VALUES (@col)", [
    new MySqlParameter("col", value)
]);

// Synchronous
source.ExecuteNonQuery("INSERT INTO `table` (`col`) VALUES (@col)", [
    new MySqlParameter("col", value)
]);
```

- Select

```csharp
MySqlDataSource source;

// Asynchronous
await foreach( MySqlDataReader reader in source.ReadAsync("SELECT `col` FROM `table`") ) {
    string value = reader.GetString("col");
    
    // ...
}

// Synchronous
foreach( MySqlDataReader reader in source.Read("SELECT `col` FROM `table`") ) {
    string value = reader.GetString("col");
    
    // ...
}
```

- LINQ

```csharp
MySqlDataSource source;

// Asynchronous
string[] values = await source.ReadAsync("SELECT `col` FROM `table`")
    .Select(reader => reader.GetString("col"))
    .ToArrayAsync();

// Synchronous
values = source.Read("SELECT `col` FROM `table`")
    .Select(reader => reader.GetString("col"))
    .ToArray();
```

- Nullables

```csharp
MySqlDataReader reader;

// TryGet
reader.TryGetString("col", out string? value);
reader.TryGetByte("col", out byte value);
reader.TryGetSByte("col", out sbyte value);
reader.TryGetInt16("col", out short value);
reader.TryGetUInt16("col", out ushort value);
reader.TryGetInt32("col", out int value);
reader.TryGetUInt32("col", out uint value);
reader.TryGetInt64("col", out long value);
reader.TryGetUInt64("col", out ulong value);
reader.TryGetFloat("col", out float value);
reader.TryGetDouble("col", out double value);
reader.TryGetMySqlDateTime("col", out MySqlDateTime value);
reader.TryGetDateTimeOffset("col", out DateTimeOffset value);
reader.TryGetDateTime("col", out DateTime value);
reader.TryGetDateOnly("col", out DateOnly value);

// Nullables
string? value = reader.GetNullableString("col");
byte? value = reader.GetNullableByte("col");
sbyte? value = reader.GetNullableSByte("col");
short? value = reader.GetNullableInt16("col");
ushort? value = reader.GetNullableUInt16("col");
int? value = reader.GetNullableInt32("col");
uint? value = reader.GetNullableUInt32("col");
long? value = reader.GetNullableInt64("col");
ulong? value = reader.GetNullableUInt64("col");
float? value = reader.GetNullableFloat("col");
double? value = reader.GetNullableDouble("col");
MySqlDateTime? value = reader.GetNullableMySqlDateTime("col");
DateTimeOffset? value = reader.GetNullableDateTimeOffset("col");
DateTime? value = reader.GetNullableDateTime("col");
DateOnly? value = reader.GetNullableDateOnly("col");
```

- JSON

```csharp
MySqlDataSource source;

MyObject[] values = await source.ReadAsync("SELECT `col` FROM `table`")
    .SelectMany(reader => reader.GetJsonEnumerableAsync<MyObject>("col"))
    .ToArrayAsync();

await foreach( MySqlDataReader reader in source.ReadAsync("SELECT `col` FROM `table`") ) {
    // MyObject? value = reader.GetJson<MyObject>("col");
    MyObject? value = await reader.GetJsonAsync<MyObject>("col");
}
```

## Versions

### 1.7.0

- Added `MySqlCommand` re-usability

### 1.6.0

- Added `GetBinaryString` helper method for reading binary columns

### 1.5.0

- Added `CreateCommand` to `MySqlTransaction`

### 1.4.0

- Added `table, column` getters
    - Added `GetBoolean`
    - Added `GetBytes`
- Added missing types
    - Added `Char`
    - Added `Guid`
    - Added `TimeOnly`
    - Added `TimeSpan`
    - Added `MySqlDecimal`
    - Added `MySqlGeometry`
    - Added `TextReader`
- Added `FieldType` getter

### 1.3.0

- Added `table, column` getters
    - Added `GetByte`
    - Added `GetSByte`
    - Added `GetDatetime`
    - Added `GetDateTimeOffset`
    - Added `GetDateOnly`
    - Added `GetMySqlDateTime`
    - Added `GetInt16`
    - Added `GetUInt16`
    - Added `GetInt32`
    - Added `GetUInt32`
    - Added `GetInt64`
    - Added `GetUInt64`
    - Added `GetDouble`
    - Added `GetFloat`
    - Added `GetString`
- Swapped `column, table?` signature on `GetOrdinal` to `table?, column`

### 1.2.0

- Added `table?` optional parameter for narrowing down ordinal (Allows duplicate keys in SELECT queries, if different tables share the same key)
- Added `GetOrdinal` and `TryGetOrdinal` methods
