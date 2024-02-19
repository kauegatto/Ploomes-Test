using Microsoft.EntityFrameworkCore;
using Ploomes_Test.Domain;
using Ploomes_Test.Infrastructure.Data;

namespace Ploomes_Test.Infrastructure.Repository;

public class TicketRepository(DataContext context) : ITicketRepository
{
    public IEnumerable<Ticket> Get()
    {
        return context.Tickets.ToList();
    }

    public Ticket? GetById(Guid id)
    {
        return context.Tickets.Find(id);
    }

    public Ticket Create(Ticket ticket)
    {
        context.Tickets.Add(ticket);
        context.SaveChanges();
        return ticket;
    }

    public Ticket Update(Ticket ticket)
    {
        context.Entry(ticket).State = EntityState.Modified;
        context.SaveChanges();
        return ticket;
    }

    public bool Delete(string id)
    {
        var ticket = context.Tickets.Find(id);
        if (ticket == null)
            return false;

        context.Tickets.Remove(ticket);
        context.SaveChanges();
        return true;
    }
}