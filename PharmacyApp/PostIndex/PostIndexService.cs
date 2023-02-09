using System.Text.Json;
using Microsoft.Extensions.Options;
using PharmacyApp.Options;
using PharmacyApp.Repositories;
using PharmacyApp.Services;

namespace PharmacyApp.PostIndex;

public class PostIndexService : IPostIndexService
{
    private readonly IClientRepository _clientRepository;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly PostItOptions _postItOptions;

    public PostIndexService(IClientRepository clientRepository, IHttpClientFactory httpClientFactory, IOptions<PostItOptions> options)
    {
        _clientRepository = clientRepository;
        _httpClientFactory = httpClientFactory;
        _postItOptions = options.Value;
    }

    public async Task UpdatePostIndexes()
    {
        var clients = await _clientRepository.GetClientsAsync();

        foreach (var client in clients)
        {
            var httpRequestMessage = new HttpRequestMessage(
                HttpMethod.Get,
                $"{_postItOptions.BaseUrl}?term={client.Address.Replace(" ","+")}&key={_postItOptions.Key}");

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                PostResponse? data = await JsonSerializer.DeserializeAsync<PostResponse>(contentStream);
                await _clientRepository.UpdateClientAsync(client with { PostCode = data?.data.First().post_code });
            }
        }
    }
}