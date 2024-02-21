using System.ComponentModel.DataAnnotations;

namespace Ploomes_Test.Domain.Dto.Ticket;

/// <summary>
/// Represents the data transfer object (DTO) used for assigning a ticket and transitioning its status to "in progress if it's applicable.".
/// </summary>
public record AssignTicketDto
(
    [EmailAddress(ErrorMessage = "The AssigneeEmail field is not a valid e-mail address.")]
    [Required(ErrorMessage = "The AssigneeEmail field is required.")]
    [StringLength(255, MinimumLength = 4, ErrorMessage = "The AssigneeEmail field must be a string with a minimum length of 4 and a maximum length of 255.")]
    string AssigneeEmail
){}