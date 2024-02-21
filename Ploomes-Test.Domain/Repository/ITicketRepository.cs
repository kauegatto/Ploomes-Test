namespace Ploomes_Test.Domain;

public interface ITicketRepository
{
    Task<IEnumerable<Ticket>> GetAll();
    Task<Ticket?> GetById(Guid id);
    Task<Ticket> Create(Ticket ticket);
    Task<Ticket> Update(Ticket ticket);
    Task<bool> Remove(Ticket ticket);
}