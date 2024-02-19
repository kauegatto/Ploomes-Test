using Ploomes_Test.Domain.Dto.Ticket;

namespace Ploomes_Test.Domain.Mappers;

public interface ITicketMapper
{
    Ticket FromTicketCreationDto(TicketCreationDto creationDto);
    Ticket FromTicketResponseDto(TicketResponseDto responseDto);
    TicketResponseDto FromTicket(Ticket ticket);
    IEnumerable<TicketResponseDto> FromTicket(IEnumerable<Ticket> ticket);

}