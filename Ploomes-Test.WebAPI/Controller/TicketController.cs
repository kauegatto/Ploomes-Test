using Microsoft.AspNetCore.Mvc;
using Ploomes_Test.Domain.Dto.Ticket;
using Ploomes_Test.Domain.Mappers;
using Ploomes_Test.Domain.Service;
using Ploomes_Test.WebAPI.Helpers;

namespace Ploomes_Test.WebAPI.Controller
{
    /// <summary>
    /// Gerencia operações relacionadas a tickets.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController(TicketService ticketService, ITicketMapper ticketMapper)
        : ControllerBase
    {
        /// <summary>
        /// Obtém todos os tickets.
        /// </summary>
        /// <returns>Uma lista de todos os tickets.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok(ticketMapper.FromTicketList(await ticketService.GetAll()));
        }

        /// <summary>
        /// Obtém um ticket específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do ticket a ser recuperado.</param>
        /// <returns>O ticket com o ID especificado.</returns>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await ticketService.GetTicketById(id);
            if (result.IsFailed)
                return NotFound(result.Errors);
            return Ok(ticketMapper.FromTicket(result.Value));
        }

        /// <summary>
        /// Cria um novo ticket.
        /// </summary>
        /// <param name="creationDto">Os dados do ticket a serem criados.</param>
        /// <returns>O ticket recém-criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] TicketCreationDto creationDto)
        {
            var result = await ticketService.Create(creationDto);
            if (result.IsFailed)
                return ActionResultFromError.FromResult(result);
            return CreatedAtAction(nameof(Get), new { id = result.Value.Id }, ticketMapper.FromTicket(result.Value));
        }
        
        /// <summary>
        /// Atribui um ticket a um novo responsável.
        /// </summary>
        /// <param name="id">O ID do ticket a ser atribuído.</param>
        /// <returns>O ticket atribuído.</returns>
        [HttpPost("{id:guid}/assign")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Assign([FromRoute] Guid id, [FromBody] AssignTicketDto assignTicketDto)
        {
            var result = await ticketService.Assign(id, assignTicketDto.AssigneeEmail);
            if (result.IsFailed)
                return ActionResultFromError.FromResult(result);
            return Ok(ticketMapper.FromTicket(result.Value));
        }

        /// <summary>
        /// Cancela um ticket.
        /// </summary>
        /// <param name="id">O ID do ticket a ser cancelado.</param>
        /// <param name="cancellingReason">O motivo do cancelamento.</param>
        /// <returns>O ticket cancelado.</returns>
        [HttpPost("{id:guid}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async  Task<IActionResult> Cancel(Guid id, string cancellingReason)
        {
            var result = await ticketService.Cancel(id, cancellingReason);
            if (result.IsFailed)
                return ActionResultFromError.FromResult(result);
            return Ok(ticketMapper.FromTicket(result.Value));
        }

        /// <summary>
        /// Conclui um ticket.
        /// </summary>
        /// <param name="id">O ID do ticket a ser concluído.</param>
        /// <returns>O ticket concluído.</returns>
        [HttpPost("{id:guid}/complete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Complete(Guid id)
        {
            var result = await ticketService.Complete(id);
            if (result.IsFailed)
               return ActionResultFromError.FromResult(result);
            return Ok(ticketMapper.FromTicket(result.Value));
        }
    }
}
