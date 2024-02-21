using System.ComponentModel.DataAnnotations;

namespace Ploomes_Test.Domain.Dto.Ticket;

/// <summary>
/// Represents the data transfer object (DTO) used for cancelling a ticket and transitioning its status to "Cancelled" if it's applicable.
/// </summary>
public record CancelTicketDto
(
    [Required]
    [StringLength(255, MinimumLength = 10, ErrorMessage = "Cancelling reason should be between 10 and 255 characters.")]
    string CancellingReason
){}