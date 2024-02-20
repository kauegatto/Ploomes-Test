using Microsoft.AspNetCore.Mvc;
using Ploomes_Test.Domain.Dto.Ticket;
using Ploomes_Test.Domain.Mappers;
using Ploomes_Test.Domain.Service;

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
        public async Task<ActionResult<IEnumerable<TicketResponseDto>>> Get()
        {
            return Ok(ticketMapper.FromTicketList(await ticketService.GetAll()));
        }

        /// <summary>
        /// Obtém um ticket específico pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do ticket a ser recuperado.</param>
        /// <returns>O ticket com o ID especificado.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TicketResponseDto>> Get(Guid id)
        {
            var result = await ticketService.GetTicketById(id);
            return result.Match<ActionResult>(
                ticket => Ok(ticketMapper.FromTicket(ticket)),
                ex => NotFound());
        }

        /// <summary>
        /// Cria um novo ticket.
        /// </summary>
        /// <param name="creationDto">Os dados do ticket a serem criados.</param>
        /// <returns>O ticket recém-criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TicketResponseDto>> Post([FromBody] TicketCreationDto creationDto)
        {
            var result = await ticketService.Create(creationDto);
            return result.Match<ActionResult>(
                ticket => CreatedAtAction(nameof(Get), new { id = ticket.Id }, ticketMapper.FromTicket(ticket)),
                ex => BadRequest(ex.Message));
        }
        
        /// <summary>
        /// Atribui um ticket a um novo responsável.
        /// </summary>
        /// <param name="id">O ID do ticket a ser atribuído.</param>
        /// <param name="assigneeEmail">O email do novo responsável.</param>
        /// <returns>O ticket atribuído.</returns>
        [HttpPost("{id}/assign")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TicketResponseDto>> Assign(Guid id, string assigneeEmail)
        {
            var result = await ticketService.Assign(id, assigneeEmail);
            return result.Match<ActionResult>(
                ticket => Ok(ticketMapper.FromTicket(ticket)),
                ex => NotFound());
        }

        /// <summary>
        /// Cancela um ticket.
        /// </summary>
        /// <param name="id">O ID do ticket a ser cancelado.</param>
        /// <param name="cancellingReason">O motivo do cancelamento.</param>
        /// <returns>O ticket cancelado.</returns>
        [HttpPost("{id}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TicketResponseDto>> Cancel(Guid id, string cancellingReason)
        {
            var result = await ticketService.Cancel(id, cancellingReason);
            return result.Match<ActionResult>(
                ticket => Ok(ticketMapper.FromTicket(ticket)),
                ex => NotFound());
        }

        /// <summary>
        /// Conclui um ticket.
        /// </summary>
        /// <param name="id">O ID do ticket a ser concluído.</param>
        /// <returns>O ticket concluído.</returns>
        [HttpPost("{id}/complete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TicketResponseDto>> Complete(Guid id)
        {
            var result = await ticketService.Complete(id);
            return result.Match<ActionResult>(
                ticket => Ok(ticketMapper.FromTicket(ticket)),
                ex => NotFound());
        }
    }
}
