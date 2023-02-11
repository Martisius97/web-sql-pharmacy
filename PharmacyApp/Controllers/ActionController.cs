using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using PharmacyApp.Migration;
using PharmacyApp.Models;
using PharmacyApp.Repositories;
using PharmacyApp.Services;

namespace PharmacyApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ActionController : ControllerBase
{
    private readonly IDbMigrationClient _dbMigrationClient;
    private readonly IImportHelper _importHelper;
    private readonly IClientRepository _clientRepository;
    private readonly IPostIndexService _postIndexService;

    public ActionController(
        IDbMigrationClient dbMigrationClient, 
        IImportHelper importHelper, 
        IClientRepository clientRepository, 
        IPostIndexService postIndexService
    )
    {
        _dbMigrationClient = dbMigrationClient;
        _importHelper = importHelper;
        _clientRepository = clientRepository;
        _postIndexService = postIndexService;
    }
    
    [HttpPost("importClients", Name = "ImportClients")]
    public async Task ImportClients(IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var fileBytes = ms.ToArray();
        var data = Encoding.Default.GetString(fileBytes);
        var clients = JsonSerializer.Deserialize<List<Client>>(data);
        await _importHelper.ImportClientsAsync(clients);
    }
    
    [HttpGet("getClients", Name = "GetClients")]
    public async Task<List<Client>> GetClients()
    {
        return await _clientRepository.GetClientsAsync();
    }
    
    [HttpGet("updatePostIndexes", Name = "UpdatePostIndexes")]
    public async Task UpdatePostIndexes()
    {
        await _postIndexService.UpdatePostIndexes();
    }

    [HttpGet("initiateDatabase", Name = "InitiateDatabase")]
    public void InitiateDatabase()
    {
        _dbMigrationClient.PerformMigrations();
    }
}