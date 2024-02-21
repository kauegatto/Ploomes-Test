using FluentResults;
using Ploomes_Test.Domain.Exceptions;
using Ploomes_Test.Domain.Models;

namespace Ploomes_Test.Domain;

public class Ticket
{
    protected Ticket() // Entity Framework
    { }
    public Ticket(string requesterEmail, string subject, string description)
    {
        Id = Guid.NewGuid();
        RequesterEmail = requesterEmail;
        AssigneeEmail = null;
        Status = TicketStatus.Created;
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
    public string? AssigneeEmail { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public string? CancellingReason { get; set; }
    public TicketStatus Status { get; set; }
    private DateTimeOffset CreateDate { get; set; }
    private DateTimeOffset UpdateDate { get; set; }
    private DateTimeOffset? StartedAt { get; set; }
    private DateTimeOffset? EndedAt { get; set; }
    public Result Assign(string assigneeEmail)
    {
        if (Status is not (TicketStatus.Created or TicketStatus.InProgress))
        {
            var msg = $"Invalid status: Tickets in {Status} cannot be assigned";
            return Result.Fail(new ValidationError(msg));
        }
        AssigneeEmail = assigneeEmail;
        Status = TicketStatus.InProgress;
        if(StartedAt is null){
            StartedAt = DateTimeOffset.Now;
        }
        return Result.Ok();
    }
    public Result Complete()
    {
        if (Status != TicketStatus.InProgress)
        {
            var msg = $"Invalid status: Tickets in {Status} cannot be completed";
            return Result.Fail(new ValidationError(msg));
        }

        Status = TicketStatus.Completed;
        EndedAt = DateTimeOffset.Now;

        return Result.Ok();
    }
    public Result Cancel(string cancellingReason)
    {
        if (Status is (TicketStatus.Completed or TicketStatus.Cancelled))
        {
            var ex = $"Invalid status: Tickets in {Status} cannot be cancelled";
            return Result.Fail(ex);
        }
        Status = TicketStatus.Completed;
        EndedAt = DateTimeOffset.Now;
        CancellingReason = cancellingReason;
        return Result.Ok();
    }
}