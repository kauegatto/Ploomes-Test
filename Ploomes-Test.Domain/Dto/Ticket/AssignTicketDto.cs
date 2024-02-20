using System.ComponentModel.DataAnnotations;

namespace Ploomes_Test.Domain.Dto.Ticket;

public record AssignTicketDto(
    [EmailAddress]
    [Required]
    [StringLength(255,MinimumLength = 4)]
    string AssigneeEmail
){ }