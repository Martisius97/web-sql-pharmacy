using Microsoft.Extensions.Options;

namespace PharmacyApp.Options;

public class SqlConnectionOptionsValidator : IValidateOptions<SqlConnectionOptions>
{
    public ValidateOptionsResult Validate(string name, SqlConnectionOptions options)
    {
        if (string.IsNullOrEmpty(options.SqlConnectionString))
        {
            return ValidateOptionsResult.Fail("\"SQLConnectionString\" is required.");
        }
        return ValidateOptionsResult.Success;
    }
}