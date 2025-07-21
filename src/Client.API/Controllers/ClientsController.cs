using Client.Application.Commands;
using Client.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Client.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Endpoint para criar um novo cliente
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] CreateClientCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(CreateClient), response);
        }
    }
}
