using System.ComponentModel.DataAnnotations;

namespace SeoAlpha.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O Email e obrigatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha e obrigatoria")]
        public string Senha { get; set; }
    }
}