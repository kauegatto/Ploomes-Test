using Ploomes_Test.Domain.Dto.Ticket;

namespace Ploomes_Test.Domain.Mappers;

public interface ITicketMapper
{
    Ticket FromTicketCreationDto(TicketCreationDto creationDto);
    Ticket FromTicketResponseDto(TicketResponseDto responseDto);
}