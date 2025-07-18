using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Client.Domain.Entities
{
    public class Client
    {
        public Guid Id { get; set; }  // Identificador �nico do cliente
        public string FullName { get; set; } = string.Empty;  // Nome completo
        public string Email { get; set; } = string.Empty;  // E-mail para login e comunica��o
        public string CPF { get; set; } = string.Empty;  // Documento �nico brasileiro
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

        // M�todo simples para valida��o futura
        public bool VerifyPassword(string hashToCompare)
        {
            return PasswordHash == hashToCompare;  // Pode ser substitu�do por verifica��o com hash real
        }
    }
}
