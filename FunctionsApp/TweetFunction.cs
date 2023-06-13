using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Text;
using System.Web.Http;
using FunctionApp1.Servicios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Tweetinvi.Core.Extensions;

namespace FunctionsApp

{
    public static class TweetFunction
    {
        

        [FunctionName("TweetFunction")]
        public static async Task<ActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)]
            HttpRequest req,
            ILogger log)
        {

            MensajeService _mensajeService = new MensajeService();


            //obtengo el req.body
            var reqParams = await req.ReadAsStringAsync();
            //lo convierto a objeto JSON
            var paramsObj = (JObject)JsonConvert.DeserializeObject(reqParams);


            //almaceno los parametros
            string userName = paramsObj.SelectToken("name")?.Value<string>();
            int? edad = paramsObj.SelectToken("age")?.Value<int>();
            string mensajeRecibido = paramsObj.SelectToken("message")?.Value<string>();
            string mail = paramsObj.SelectToken("mail")?.Value<string>();


            //Si el mensaje está vacío tirar un error
            if (mensajeRecibido.IsNullOrEmpty())
            {
                Console.WriteLine();
                var Result = new BadRequestErrorMessageResult("El mensaje no llegó, o estaba vacío");

                return Result;
            }

            ////
            
            //armo el mensaje a mandar
            StringBuilder mensajeAEnviar = new();

            if ( ! userName.IsNullOrEmpty() )
                mensajeAEnviar.Append(userName);
            else
                mensajeAEnviar.Append("Anónimo");

            mensajeAEnviar.Append("\n");

            if (edad != null)
            {
                mensajeAEnviar.Append(edad);
                mensajeAEnviar.Append(" años");
                mensajeAEnviar.Append("\n");
            }

            mensajeAEnviar.Append(mensajeRecibido);

            /////


            //posteo el twit
            try
            {
                _mensajeService.MakeATweet(mensajeAEnviar.ToString());
                return new OkResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new BadRequestErrorMessageResult(e.ToString());

            }


        }
    }
    
}
