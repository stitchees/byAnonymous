using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GrupoAzureWebIII.Models
{
    public class Mensaje
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String mensaje { set; get; }
        public String medioEnvio { set; get; }
        public String remitente { get; set; }
        public String destinatario { get; set; }

    }
}