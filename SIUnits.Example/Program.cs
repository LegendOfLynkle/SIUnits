using Npgsql;
using SIUnits;
using SIUnits.Npgsql;

var connectionString = "Host=localhost;Username=postgres;Password=password;Database=test";
var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
#pragma warning disable NPG9001
dataSourceBuilder.UseSIUnits();
#pragma warning restore NPG9001

var dataSource = dataSourceBuilder.Build();
var conn = await dataSource.OpenConnectionAsync();

var unit = new SIUnit { Value = 1.0, Units = [1, 0, 0, 0, 0, 0, 0, 0] }; // would represent 1 meter
await using (var cmd = new NpgsqlCommand("CREATE TEMP TABLE data (unit unit)", conn))
    await cmd.ExecuteNonQueryAsync();

// Insert some data
await using (var cmd = new NpgsqlCommand("INSERT INTO data (unit) VALUES (@u)", conn))
{
     cmd.Parameters.AddWithValue("@u", unit);
     await cmd.ExecuteNonQueryAsync();
}

// Retrieve all rows
await using (var cmd = new NpgsqlCommand("SELECT unit FROM data", conn))
await using (var reader = await cmd.ExecuteReaderAsync())
{
    while (await reader.ReadAsync())
    {
        var res = reader.GetFieldValue<SIUnit>(0);
        Console.WriteLine($"Value: {res.Value}, Unit:");
        for (var ii = 0; ii < res.Units.Length; ii++)
        {
            var item = res.Units[ii];
            Console.WriteLine($"[{ii}]: {item}");
        }
    }
}