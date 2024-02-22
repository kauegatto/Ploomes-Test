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
        [HttpGet(Name="Get all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<TicketResponseDto>))]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketResponseDto))]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TicketResponseDto))]
        public async Task<IActionResult> Post([FromBody] TicketCreationDto creationDto)
        {
            var result = await ticketService.Create(creationDto);
            return result.IsFailed ? ActionResultFromError.FromResult(HttpContext, result) : CreatedAtAction(nameof(Get), new { id = result.Value.Id }, ticketMapper.FromTicket(result.Value));
        }
        
        /// <summary>
        /// Atribui um ticket a um novo responsável.
        /// </summary>
        /// <param name="id">O ID do ticket a ser atribuído.</param>
        /// <returns>O ticket atribuído.</returns>
        [HttpPost("{id:guid}/assign")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketResponseDto))]
        [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Assign([FromRoute] Guid id, [FromBody] AssignTicketDto assignTicketDto)
        {
            var result = await ticketService.Assign(id, assignTicketDto.AssigneeEmail);
            return result.IsFailed ? ActionResultFromError.FromResult(HttpContext, result) : Ok(ticketMapper.FromTicket(result.Value));
        }

        /// <summary>
        /// Cancela um ticket.
        /// </summary>
        /// <param name="id">O ID do ticket a ser cancelado.</param>
        /// <param name="cancelTicketDto">O dto que contém o motivo do cancelamento.</param>
        /// <returns>O ticket cancelado.</returns>
        [HttpPost("{id:guid}/cancel")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketResponseDto))]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async  Task<IActionResult> Cancel([FromRoute] Guid id, [FromBody] CancelTicketDto cancelTicketDto)
        {
            var result = await ticketService.Cancel(id, cancelTicketDto.CancellingReason);
            return result.IsFailed ? ActionResultFromError.FromResult(HttpContext, result) : Ok(ticketMapper.FromTicket(result.Value));
        }

        /// <summary>
        /// Conclui um ticket.
        /// </summary>
        /// <param name="id">O ID do ticket a ser concluído.</param>
        /// <returns>O ticket concluído.</returns>
        [HttpPost("{id:guid}/complete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketResponseDto))]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Complete([FromRoute] Guid id)
        {
            var result = await ticketService.Complete(id);
             return result.IsFailed ? ActionResultFromError.FromResult(HttpContext, result) : Ok(ticketMapper.FromTicket(result.Value));
        }
        
        /// <summary>
        /// Exclui um ticket pelo seu ID.
        /// </summary>
        /// <param name="id">O ID do ticket a ser excluído.</param>
        /// <returns>Um IAction Result de Ok, se a exclusão der certo, e um de 404 caso não exista.</returns>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TicketResponseDto))]
        [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await ticketService.Delete(id);
            return result.IsFailed ? ActionResultFromError.FromResult(HttpContext, result) : Ok();
        }
    }
}
