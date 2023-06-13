using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

public class AzureFunctionTuSecreto
{
    private readonly HttpClient _httpClient;

    public AzureFunctionTuSecreto()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> CallAzureFunction(string? message, string? nombre)
    {
        var url = "https://connecttoapi.azurewebsites.net/api/TweetFunction";
        var key = "g1Abi09Xg_JLpk-R41dhvHmTwp_FXWwHbaDMh-o7XBdzAzFugsmrCw==";

        var payload = new
        {
            nombre= "Ejemplo",
            mensaje = "Hola desde el cliente"
        };
        _httpClient.DefaultRequestHeaders.Add("x-functions-key", key);
        var jsonPayload = JsonSerializer.Serialize(payload);
        var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json"); 
        var response = await _httpClient.PostAsync(url, content);  
        if (response.IsSuccessStatusCode)
        {
           await response.Content.ReadAsStringAsync();
            return  ("success");
        }
        return ("error");
    }
}