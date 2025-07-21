using System.ComponentModel.DataAnnotations;

namespace Client.Application.DTOs
{
    public class CreateClientRequest
    {
        [Required(ErrorMessage = "O nome completo � obrigat�rio.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email � obrigat�rio.")]
        [EmailAddress(ErrorMessage = "O email informado n�o � v�lido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF � obrigat�rio.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 d�gitos num�ricos.")]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha � obrigat�ria.")]
        [MinLength(6, ErrorMessage = "A senha deve conter pelo menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "O n�mero de telefone � obrigat�rio.")]
        [RegularExpression(@"^\(?[1-9]{2}\)?\s?(?:9\d{4}|\d{4})-?\d{4}$", ErrorMessage = "O n�mero de telefone deve conter um DDD v�lido e seguir o formato brasileiro.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
