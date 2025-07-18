using System;

using MediatR;
using Client.Application.DTOs;

namespace Client.Application.Commands
{
    public class CreateClientCommand : IRequest<Guid>
    {
        public CreateClientRequest Request { get; }

        public CreateClientCommand(CreateClientRequest request)
        {
            Request = request;
        }
    }
}
