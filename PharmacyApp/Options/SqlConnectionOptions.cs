using Microsoft.Extensions.Options;

namespace PharmacyApp.Options;

public class SqlConnectionOptions : IOptions<SqlConnectionOptions>
{
    SqlConnectionOptions IOptions<SqlConnectionOptions>.Value => this;
    
    public string? SqlConnectionString { get; set; }
}