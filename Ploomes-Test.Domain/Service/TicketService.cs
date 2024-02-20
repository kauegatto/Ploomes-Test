using LanguageExt.Common;
using Ploomes_Test.Domain.Dto.Ticket;
using Ploomes_Test.Domain.Exceptions;
using Ploomes_Test.Domain.Mappers;

namespace Ploomes_Test.Domain.Service;

public class TicketService(ITicketRepository ticketRepository, ITicketMapper ticketMapper)
{
    public async Task<Result<Ticket>> Assign(Guid ticketId, string assigneeEmail)
    {
        var ticketResult = await GetTicketById(ticketId);

        return await ticketResult.Match<Task<Result<Ticket>>>(
            async ticket =>
            {
                var assignResult = ticket.Assign(assigneeEmail);
                if (assignResult.IsFaulted)
                    return assignResult;

                return await UpdateTicket(ticket);
            },
            ex => Task.FromResult(new Result<Ticket>(ex))
        );
    }

    public async Task<Result<Ticket>> Cancel(Guid ticketId, string cancellingReason)
    {
        var ticketResult = await GetTicketById(ticketId);
        return ticketResult.Match(
            ticket => ticket.Cancel(cancellingReason),
            ex => new Result<Ticket>(ex));
    }
    
    public async Task<Result<Ticket>> Complete(Guid ticketId)
    {
        var ticketResult = await GetTicketById(ticketId);
        return ticketResult.Match(
            ticket => ticket.Complete(),
            ex => new Result<Ticket>(ex));
    }
    
    public async Task<Result<Ticket>> Create(TicketCreationDto creationDto)
    {
        var ticket = ticketMapper.FromTicketCreationDto(creationDto);
        var created = await ticketRepository.Create(ticket);
        return created;
    }
    
    public async Task<Result<Ticket>> GetTicketById(Guid ticketId)
    {
        var ticket = await ticketRepository.GetById(ticketId);
        if (ticket is null)
        {
            var ex = new ValidationException($"Ticket with id {ticketId} not found");
            return new Result<Ticket>(ex);
        }

        return ticket;
    }
    public async Task<IEnumerable<Ticket>> GetAll()
    {
        return await ticketRepository.GetAll();
    }
    public async Task<Result<Ticket>> UpdateTicket(Ticket ticket)
    {
        return new Result<Ticket>(await ticketRepository.Update(ticket));
    }
}