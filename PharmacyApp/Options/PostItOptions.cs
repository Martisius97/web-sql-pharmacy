using Microsoft.Extensions.Options;

namespace PharmacyApp.Options;

public class PostItOptions : IOptions<PostItOptions>
{
    PostItOptions IOptions<PostItOptions>.Value => this;
    
    public string BaseUrl { get; set; }
    public string Key { get; set; }
}