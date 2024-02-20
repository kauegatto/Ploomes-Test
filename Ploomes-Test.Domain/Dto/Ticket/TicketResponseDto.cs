namespace Ploomes_Test.Domain.Dto.Ticket;

public record TicketResponseDto(
    Guid Id,
    string RequesterEmail,
    string Status,
    string Subject,
    string Description)
{ }