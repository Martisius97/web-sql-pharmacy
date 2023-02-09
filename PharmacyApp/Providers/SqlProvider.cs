using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using PharmacyApp.Options;

namespace PharmacyApp.Providers;

public class SqlProvider : ISqlProvider
{
    private readonly SqlConnection _sqlConnection;

    public SqlProvider(IOptions<SqlConnectionOptions> options)
    {
        _sqlConnection = new SqlConnection(options.Value.SqlConnectionString);
    }

    public SqlConnection GetConnection()
    {
        return _sqlConnection;
    }
}