using System;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;
namespace GrupoAzureWebIII.Services
{
    public class EmailService
    {
        private readonly HttpClient httpClient;
        private readonly string emailFunctionUrl;
        private readonly string apiKey;

        public EmailService(string emailFunctionUrl, string apiKey)
        {
            // Crear una instancia de HttpClient
            this.httpClient = new HttpClient();
            this.apiKey = apiKey;
            this.emailFunctionUrl = emailFunctionUrl;
        }

        public async Task<bool> EnviarEmail(string mensaje,string user)
        {
            try
            {
                // Crea el contenido del cuerpo (body) de la solicitud
                string requestBody = $"{{\"message\":\"{mensaje}\",\"user\":\"{user}\"}}";

                // Agrega los encabezados (headers) de la solicitud
                httpClient.DefaultRequestHeaders.Add("x-functions-key", apiKey);

                // Hacer la solicitud HTTP al endpoint de la función "SendEmail"
                HttpResponseMessage response = await httpClient.PostAsync(emailFunctionUrl,
                                                new StringContent(requestBody, Encoding.UTF8, "application/json"));

                // Verificar el estado de la respuesta HTTP
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Se realizó el mail se envio correctamente");
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(jsonResponse);
                    // HABRÍA QUE GUARDAR EL MAIL SI SE MANDA DE MANERA CORRECTA

                    return true;
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
                Console.WriteLine($"Error al enviar el correo electrónico: {ex.Message}");
                return false;
            }
        }
    }

}
