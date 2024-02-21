namespace Ploomes_Test.Domain.Dto.Ticket;

public record TicketResponseDto(
    Guid Id,
    string RequesterEmail,
    string AssigneeEmail,
    string Status,
    string Subject,
    string Description,
    DateTimeOffset CreateDate
    )
{ }