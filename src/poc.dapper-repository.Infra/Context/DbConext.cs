using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace poc.dapper_repository.Infra.Context;

public class DbConext
{
    private readonly SqlConnection _connection;

    public DbConext(IConfiguration configuration)
    {
        _connection = new SqlConnection(configuration.GetConnectionString("DbConnection"));
    }

    public async Task<IDbConnection> CreateConnectionAsync()
    {
        await _connection.OpenAsync();
        return _connection;
    }

    public async Task CloseConnection()
    {
        await _connection.CloseAsync();
    }
}
