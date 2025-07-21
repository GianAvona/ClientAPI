using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Client.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; init; }  // Identificador �nico do cliente
        public string FullName { get; set; } = string.Empty;  // Nome completo
        public string Email { get; set; } = string.Empty;  // E-mail para login e comunica��o
        public string CPF { get; set; } = string.Empty;  // Documento �nico brasileiro
        public string PasswordHash { get; set; } = string.Empty;  // Hash da senha
        public string PhoneNumber { get; set; } = string.Empty;  // Telefone de contato

        public Client() { }

        public Client(string fullName, string email, string cpf, string password, string phoneNumber)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email;
            CPF = cpf;
            PasswordHash = HashPwd(password);
            PhoneNumber = phoneNumber;
        }

        private string HashPwd(string password)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(password, "Senha não pode ser nula ou vazia");

            using var sha256 = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
