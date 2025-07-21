using System.ComponentModel.DataAnnotations;

namespace Client.Application.DTOs
{
    public class CreateClientRequest
    {
        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email informado não é válido.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "O CPF deve conter exatamente 11 dígitos numéricos.")]
        public string CPF { get; set; } = string.Empty;

        [Required(ErrorMessage = "A senha é obrigatória.")]
        [MinLength(6, ErrorMessage = "A senha deve conter pelo menos 6 caracteres.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "O número de telefone é obrigatório.")]
        [RegularExpression(@"^\(?[1-9]{2}\)?\s?(?:9\d{4}|\d{4})-?\d{4}$", ErrorMessage = "O número de telefone deve conter um DDD válido e seguir o formato brasileiro.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
