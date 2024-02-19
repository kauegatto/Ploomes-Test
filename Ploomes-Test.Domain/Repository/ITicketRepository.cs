namespace Ploomes_Test.Domain;

public interface ITicketRepository
{
    public IEnumerable<Ticket> Get();
    public Ticket? GetById(Guid id);
    public Ticket Create(Ticket ticket);
    public Ticket Update(Ticket ticket);
    public bool Delete(string id);
}