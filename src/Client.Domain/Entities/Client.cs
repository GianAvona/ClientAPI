using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Client.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; set; }  // Identificador único do cliente
        public string FullName { get; set; } = string.Empty;  // Nome completo
        public string Email { get; set; } = string.Empty;  // E-mail para login e comunicação
        public string CPF { get; set; } = string.Empty;  // Documento único brasileiro
        public string PasswordHash { get; set; } = string.Empty;  // Hash da senha
        public string PhoneNumber { get; set; } = string.Empty;  // Telefone de contato

        public Client() { }

        public Client(string fullName, string email, string cpf, string passwordHash, string phoneNumber)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            Email = email;
            CPF = cpf;
            PasswordHash = passwordHash;
            PhoneNumber = phoneNumber;
        }

        // Método simples para validação futura
        public bool VerifyPassword(string hashToCompare)
        {
            return PasswordHash == hashToCompare;  // Pode ser substituído por verificação com hash real
        }
    }
}
