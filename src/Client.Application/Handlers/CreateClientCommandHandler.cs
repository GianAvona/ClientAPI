using Client.Application.Commands;
using Client.Domain.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            var client = new Domain.Entities.Client(
                command.FullName,
                command.Email,
                command.CPF,
                command.Password,
                command.PhoneNumber
            );

            await _clientRepository.AddAsync(client);

            return client.Id;
        }
    }
}
