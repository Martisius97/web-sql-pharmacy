using Microsoft.Extensions.Options;

namespace PharmacyApp.Options;

public class PostItOptionsValidator : IValidateOptions<PostItOptions>
{
    public ValidateOptionsResult Validate(string name, PostItOptions options)
    {
        if (string.IsNullOrEmpty(options.BaseUrl))
        {
            return ValidateOptionsResult.Fail("\"BaseUrl\" is required.");
        }
        
        if (string.IsNullOrEmpty(options.Key))
        {
            return ValidateOptionsResult.Fail("\"Key\" is required.");
        }
        
        return ValidateOptionsResult.Success;
    }
}