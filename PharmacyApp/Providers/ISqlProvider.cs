using Microsoft.Data.SqlClient;

namespace PharmacyApp.Providers;

public interface ISqlProvider
{
    SqlConnection GetConnection();
}