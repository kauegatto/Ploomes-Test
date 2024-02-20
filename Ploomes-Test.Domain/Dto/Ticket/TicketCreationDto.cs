using System.ComponentModel.DataAnnotations;

namespace Ploomes_Test.Domain.Dto.Ticket;

public record TicketCreationDto
(
    [Required(ErrorMessage = "RequesterEmail must be present")]
    [EmailAddress]
    string RequesterEmail,
    [Required(ErrorMessage = "Subject must be present")]
    [StringLength(255)]
    string Subject,
    [Required(ErrorMessage = "Description must be present")]
    [StringLength(255)]
    string Description
){}