using System;
using System.Text;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Client.Application.Commands;
using Client.Domain.Interfaces;
using MediatR;

namespace Client.Application.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IClientRepository _clientRepository;

        public LoginCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            if (string.IsNullOrWhiteSpace(request.Email) && string.IsNullOrWhiteSpace(request.CPF))
                throw new ArgumentException("Informe um email ou CPF para login.");

            var client = !string.IsNullOrWhiteSpace(request.Email)
                ? await _clientRepository.GetByEmailAsync(request.Email)
                : await _clientRepository.GetByCpfAsync(request.CPF!);

            if (client == null)
                throw new InvalidOperationException("Email/CPF ou senha inválidos.");

            //  Gerar hash da senha fornecida
            using var sha256 = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(request.Password);
            var hashBytes = sha256.ComputeHash(passwordBytes);
            var inputPasswordHash = Convert.ToBase64String(hashBytes);

            if (inputPasswordHash != client.PasswordHash)
                throw new InvalidOperationException("Email/CPF ou senha inválidos.");

            // Retorno fictício (ex: token)
            return "Login efetuado com sucesso"; // ou return jwtService.GenerateToken(client)
        }
    }
}
