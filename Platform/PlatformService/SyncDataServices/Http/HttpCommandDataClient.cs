using PlatformModels.Dtos;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace PlatformService.SyncDataService.Http;

public class HttpCommandDataClient :ICommandDataClient {

    private readonly HttpClient _http;
    private IConfiguration _config;

    public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration) {
        _http = httpClient;
        _config = configuration;
        
    }

    public async Task SendPlatformToCommand(PlatformDto platform) {
        var httpContent = new StringContent(
            JsonSerializer.Serialize(platform),
            Encoding.UTF8,
            "application/json"
        );

        var baseUrl = _config["CommandService"];

        
        var resp = await _http.PostAsync($"{baseUrl}/api/c/platforms", httpContent);

        if (resp.IsSuccessStatusCode) {
            Console.WriteLine("--> Sync POST to Command Service Ok");
        }
        else {
            Console.WriteLine("--> Sync POST to Command Service Failed");
        }

        return;

    }
}
