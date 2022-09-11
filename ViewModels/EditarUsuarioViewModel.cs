using System.ComponentModel.DataAnnotations;

namespace SeoAlpha.ViewModels
{
    public class EditarUsuarioViewModel
    {
        [Required(ErrorMessage = "O nome e obrrigatorio")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Esse campo deve conter entre 3 e 40 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "A senha e obrigatoria")]
        public string Senha { get; set; }
    }
}
