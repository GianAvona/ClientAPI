using System.ComponentModel.DataAnnotations;

namespace Client.Application.DTOs
{
    public class LoginRequest
    {
        [EmailAddress(ErrorMessage = "O email informado n�o � v�lido.")]
        public string? Email { get; set; }

        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 d�gitos num�ricos.")]
        public string? CPF { get; set; }

        [Required(ErrorMessage = "A senha � obrigat�ria.")]
        [MinLength(6, ErrorMessage = "A senha deve conter pelo menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;
    }
}
