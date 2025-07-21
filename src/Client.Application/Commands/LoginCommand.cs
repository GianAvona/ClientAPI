using Client.Application.DTOs;
using MediatR;

namespace Client.Application.Commands
{
    // O LoginCommand retorna um token (string) ou uma exceção, a depender da lógica que você aplicar
    public class LoginCommand : IRequest<string>
    {
        public LoginRequest Request { get; }

        public LoginCommand(LoginRequest request)
        {
            Request = request;
        }
    }
}
