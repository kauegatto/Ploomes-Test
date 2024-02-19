using Microsoft.EntityFrameworkCore;
using Ploomes_Test.Domain;
using Ploomes_Test.Infrastructure.Data;

namespace Ploomes_Test.Infrastructure.Repository;
public class TicketRepository(DataContext context) : ITicketRepository
{
    public async Task<IEnumerable<Ticket>> GetAll()
    {
        return await context.Tickets.ToListAsync();
    }

    public async Task<Ticket?> GetById(Guid id)
    {
        return await context.Tickets.FindAsync(id);
    }

    public async Task<Ticket> Create(Ticket ticket)
    {
        await context.Tickets.AddAsync(ticket);
        await context.SaveChangesAsync();
        return ticket;
    }

    public async Task<Ticket> Update(Ticket ticket)
    {
        context.Entry(ticket).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return ticket;
    }

    public async Task<bool> Remove(Guid id)
    {
        var ticket = await context.Tickets.FindAsync(id);
        if (ticket == null)
            return false;

        context.Tickets.Remove(ticket);
        await context.SaveChangesAsync();
        return true;
    }
}