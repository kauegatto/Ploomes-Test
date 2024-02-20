using System.ComponentModel.DataAnnotations;
using LanguageExt.Common;
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
        StartedAt = null;
        EndedAt = null;
        CancellingReason = null;
        Subject = subject;
        Description = description;
    }
    public Guid Id { get; set; }
    public string RequesterEmail { get; set; }
    public string AssigneeEmail { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public string? CancellingReason { get; set; }
    public TicketStatus Status { get; set; } = TicketStatus.Created;
    private DateTimeOffset CreateDate { get; set; }
    private DateTimeOffset UpdateDate { get; set; }
    private DateTimeOffset? StartedAt { get; set; }
    private DateTimeOffset? EndedAt { get; set; }
    public Result<Ticket> Assign(string assigneeEmail)
    {
        if (Status is not (TicketStatus.Created or TicketStatus.InProgress))
        {
            var ex = new ValidationException($"Invalid status: Tickets in {Status} cannot be assigned");
            return new Result<Ticket>(ex);
        }
        AssigneeEmail = assigneeEmail;
        if(StartedAt is null){
            StartedAt = DateTimeOffset.Now;
        }
        return this;
    }
    public Result<Ticket> Complete()
    {
        if (Status != TicketStatus.InProgress)
        {
            var ex = new ValidationException($"Invalid status: Tickets in {Status} cannot be completed");
            return new Result<Ticket>(ex);
        }

        Status = TicketStatus.Completed;
        EndedAt = DateTimeOffset.Now;

        return this;
    }
    public Result<Ticket> Cancel(string cancellingReason)
    {
        if (Status is (TicketStatus.Completed or TicketStatus.Cancelled))
        {
            var ex = new ValidationException($"Invalid status: Tickets in {Status} cannot be cancelled");
            return new Result<Ticket>(ex);
        }
        Status = TicketStatus.Completed;
        EndedAt = DateTimeOffset.Now;
        CancellingReason = cancellingReason;
        return this;
    }
}