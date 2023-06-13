using GrupoAzureWebIII.Models;
using GrupoAzureWebIII.ViewModels;

namespace GrupoAzureWebIII.Services
{
    public class DbService
    {
        private readonly MensajeDbContext _dbContext;

        public DbService(MensajeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CrearModel(FormViewModel form)
        {
                
            string mensaje = form.mensaje.ToString();
            string medioDeEnvio;
            string remitente;
            string destinatario;

            if (form.apodo != null)
                remitente = form.apodo.ToString();
            else
                remitente = "Anónimo";

            if (form.user != null)
                destinatario = form.user.ToString();
            else
                destinatario = "Destinatario no especificado";

            if (form.publicarTwitter)
                medioDeEnvio = "twitter";
            else if (form.publicarEmail)
                medioDeEnvio = "email";
            else
                return false;

            var mensajeEntity = new Mensaje
            {
                mensaje = mensaje,
                medioEnvio = medioDeEnvio,
                remitente = remitente,
                destinatario = destinatario
            };

            try
            {

                _dbContext.Mensaje.Add(mensajeEntity);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public List<Mensaje> ObtenerMensajes()
        {
            return _dbContext.Mensaje.ToList();
        }


    }
}
