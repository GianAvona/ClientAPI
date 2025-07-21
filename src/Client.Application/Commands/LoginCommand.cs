using Client.Application.DTOs;
using MediatR;

namespace Client.Application.Commands
{
    // O LoginCommand retorna um token (string) ou uma exce��o, a depender da l�gica que voc� aplicar
    public class LoginCommand : IRequest<string>
    {
        public LoginRequest Request { get; }

        public LoginCommand(LoginRequest request)
        {
            Request = request;
        }
    }
}
