using System.ComponentModel.DataAnnotations;

namespace SeoAlpha.ViewModels
{
    public class CriarUsuarioViewModel
    {
        [Required(ErrorMessage = "O nome e obrigatorio")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Esse campo deve conter entre 3 e 40 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O Email e obrigatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha e obrigatoria")]
        public string Senha { get; set; }
    }
}
