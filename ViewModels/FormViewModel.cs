using System.ComponentModel.DataAnnotations;

namespace byAnonymous.ViewModels
{
    public class FormViewModel
    {
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string mensaje { get; set; }
        public bool publicarTwitter { get; set; }
        public bool publicarEmail { get; set; }
        public string? apodo { get; set; }
        public string? user { get; set; }
 
    }
}
