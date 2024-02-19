using System.ComponentModel.DataAnnotations;

namespace Ploomes_Test.Domain.Dto.Ticket;

public record TicketCreationDto
(
    [Required(ErrorMessage = "Email não pode ser vazio")]
    string RequesterEmail,
    [Required(ErrorMessage = "Título não pode ser vazio")]
    string Subject,
    [Required(ErrorMessage = "Descrição não pode ser vazia")]
    string Description
){}