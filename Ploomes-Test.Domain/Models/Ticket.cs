using Ploomes_Test.Domain.Models;

namespace Ploomes_Test.Domain;

public class Ticket
{
    protected Ticket() // Entity Framework
    { }

    public Ticket(string requesterEmail, string assigneeEmail, TicketStatus status, string subject, string description)
    {
        Id = Guid.NewGuid();
        RequesterEmail = requesterEmail;
        AssigneeEmail = assigneeEmail;
        Status = status;
        CreateDate = DateTimeOffset.Now;
        UpdateDate = DateTimeOffset.Now;
        Subject = subject;
        Description = description;
    }
    public Guid Id { get; set; }
    public string RequesterEmail { get; set; }
    public string AssigneeEmail { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.Created;
    private DateTimeOffset CreateDate { get; }
    private DateTimeOffset UpdateDate { get; }
}