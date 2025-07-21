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
        public async Task<IActionResult> CreateClient([FromBody] CreateClientRequest request)
        {
            //  Validação de modelo (formato)
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var command = new CreateClientCommand(request);
                var clientId = await _mediator.Send(command);

                return CreatedAtAction(nameof(GetClientById), new { id = clientId }, new { id = clientId });
            }
            catch (InvalidOperationException ex)
            {
                //  CPF ou Email já existentes
                return Conflict(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                // Outros erros esperados (ex: senha vazia)
                return BadRequest(new { message = ex.Message });
            }
        }

        // Endpoint de exemplo futuro: buscar cliente por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(Guid id)
        {
            // Simulação: endpoint reservado para ser implementado depois
            return Ok(new { message = $"Simulação de retorno para o cliente com ID {id}" });
        }
    }
}
