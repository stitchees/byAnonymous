using System;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace GrupoAzureWebIII
{
    public class TwitterService
    {
        private readonly HttpClient httpClient;
        private readonly string tweetFunctionUrl;
        private readonly string apiKey;

        public TwitterService(string tweetFunctionUrl, string apiKey)
        {
            this.tweetFunctionUrl = tweetFunctionUrl;
            this.apiKey = apiKey;
            httpClient = new HttpClient();
        }

        public async Task<bool> PublicarTweet(string mensaje, string? apodo,string? user)
        {


            try
            {
                // Crea el contenido del cuerpo (body) de la solicitud
                string requestBody = generarBody(mensaje,apodo,user); 
               
                // Agrega los encabezados (headers) de la solicitud
                httpClient.DefaultRequestHeaders.Add("x-functions-key", apiKey);
               
                // Hacer la solicitud HTTP al endpoint de la función "tweetFuction"
                HttpResponseMessage response = await httpClient.PostAsync(tweetFunctionUrl,
                    new StringContent(requestBody, Encoding.UTF8, "application/json"));
               
                // Verificar el estado de la respuesta HTTP
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Se realizó el tuit correctamente");
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(jsonResponse);
                    return true;
                    // HABRÍA QUE GUARDAR EL MAIL SI SE MANDA DE MANERA CORRECTA
                }
                else
                {
                    // Si la respuesta no es exitosa, muestra el código de estado
                    Console.WriteLine($"Error en la solicitud. Código de estado: {response.StatusCode}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        private string generarBody(string mensaje, string? apodo, string? user)
        {
            if (apodo == null)
                apodo = "anonimo";

            string userString = user != null ? $"@{user}" : string.Empty;

            string body = $"{{\"message\":\"{userString} {mensaje}\",\"name\":\"by: {apodo}\"}}";

            return body;
        }
    }

}
