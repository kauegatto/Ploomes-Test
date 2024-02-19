using Microsoft.AspNetCore.Mvc;
using Ploomes_Test.Domain;
using Ploomes_Test.Domain.Dto.Ticket;
using Ploomes_Test.Domain.Mappers;

namespace Ploomes_Test.WebAPI.Controller
{
    /// <summary>
    /// Gerencia operações relacionadas a tickets.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController(ITicketRepository ticketRepository, ITicketMapper ticketMapper)
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
            var response = await ticketRepository.GetAll();
            return Ok(ticketMapper.FromTicket(response));
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
            var response = await ticketRepository.GetById(id); 
            return response != null ? Ok(ticketMapper.FromTicket(response)) : NotFound();
        }

        /// <summary>
        /// Cria um novo ticket.
        /// </summary>
        /// <param name="ticket">O objeto ticket a ser criado.</param>
        /// <returns>O ticket recém-criado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TicketResponseDto>> Post([FromBody] Ticket ticket)
        {
            var response = await ticketRepository.Create(ticket); 
            return CreatedAtAction(nameof(Get), new { id = response.Id }, ticketMapper.FromTicket(response));
        }

        /// <summary>
        /// Atualiza um ticket existente.
        /// </summary>
        /// <param name="id">O ID do ticket a ser atualizado.</param>
        /// <param name="ticket">O objeto ticket atualizado.</param>
        /// <returns>O ticket atualizado.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<TicketResponseDto>> Put(Guid id, [FromBody] Ticket ticket)
        {
            var response = await ticketRepository.Update(ticket);
            return Ok(ticketMapper.FromTicket(response));
        }

        /// <summary>
        /// Exclui um ticket pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do ticket a ser excluído.</param>
        /// <returns>True se o ticket foi excluído com sucesso, caso contrário, false.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var isDeleted = await ticketRepository.Remove(id);
            return isDeleted ? Ok(true) : NotFound(false);
        }
    }
}
