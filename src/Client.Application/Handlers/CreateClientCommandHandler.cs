using System;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

using Client.Application.Commands;
using ClientEntity = Client.Domain.Entities.Client;
using Client.Domain.Interfaces;
using MediatR;

namespace Client.Application.Handlers
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Guid>
    {
        private readonly IClientRepository _clientRepository;

        public CreateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Guid> Handle(CreateClientCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Senha n�o pode ser nula ou vazia");

            //  Verifica��o de duplicidade de Email
            var emailExists = await _clientRepository.ExistsByEmailAsync(request.Email);
            if (emailExists)
                throw new InvalidOperationException("J� existe um cliente com este email.");

            //  Verifica��o de duplicidade de CPF
            var cpfExists = await _clientRepository.ExistsByCPFAsync(request.CPF);
            if (cpfExists)
                throw new InvalidOperationException("J� existe um cliente com este CPF.");

            //  Hash da senha
            using var sha256 = SHA256.Create();
            var passwordBytes = Encoding.UTF8.GetBytes(request.Password);
            var hashBytes = sha256.ComputeHash(passwordBytes);
            var passwordHash = Convert.ToBase64String(hashBytes);

            var client = new ClientEntity(
                fullName: request.FullName,
                email: request.Email,
                cpf: request.CPF,
                passwordHash: passwordHash,
                phoneNumber: request.PhoneNumber
            );

            await _clientRepository.AddAsync(client);

            return client.Id;
        }
    }
}
