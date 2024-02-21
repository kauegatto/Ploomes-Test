using FluentResults;
using Ploomes_Test.Domain.Dto.Ticket;
using Ploomes_Test.Domain.Exceptions;
using Ploomes_Test.Domain.Mappers;

namespace Ploomes_Test.Domain.Service;

public class TicketService(ITicketRepository ticketRepository, ITicketMapper ticketMapper)
{
    public async Task<Result<Ticket>> Assign(Guid ticketId, string assigneeEmail)
    {
        Result<Ticket> ticketResult = await GetTicketById(ticketId);
        if (ticketResult.IsFailed) 
            return ticketResult;

        Ticket ticket = ticketResult.Value;
        Result assignResult = ticket.Assign(assigneeEmail);
        if (assignResult.IsSuccess)
        {
            await UpdateTicket(ticket);
            return ticket;
        }
        return assignResult;
    }

    public async Task<Result<Ticket>> Cancel(Guid ticketId, string cancellingReason)
    {
        var ticketResult = await GetTicketById(ticketId);
        if (ticketResult.IsFailed)
        {
            return ticketResult;
        }

        var ticket = ticketResult.Value;
        Result cancellationResult = ticket.Cancel(cancellingReason);
        if (cancellationResult.IsSuccess)
        {
            await UpdateTicket(ticket);
            return ticket;
        }
        return cancellationResult;
    }
    
    public async Task<Result<Ticket>> Complete(Guid ticketId)
    {
        var ticketResult = await GetTicketById(ticketId);
        if (ticketResult.IsFailed)
        {
            return ticketResult;
        }

        var ticket = ticketResult.Value;
        Result completion = ticket.Complete();
        if (completion.IsSuccess)
        {
            await UpdateTicket(ticket);
            return ticket;
        }

        return completion;
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
            var msg = $"Ticket with id {ticketId} not found";
            return Result.Fail(new NotFoundError(msg));
        }
        return ticket;
    }
    public async Task<IEnumerable<Ticket>> GetAll()
    {
        return await ticketRepository.GetAll();
    }
    public async Task<Result<Ticket>> UpdateTicket(Ticket ticket)
    {
        return await ticketRepository.Update(ticket);
    }
}