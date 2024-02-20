using AutoMapper;
using Ploomes_Test.Domain;
using Ploomes_Test.Domain.Dto.Ticket;
using Ploomes_Test.Domain.Mappers;

namespace Ploomes_Test.Infrastructure.Mappers;

public class TicketMapper(IMapper mapper) : ITicketMapper
{
    public Ticket FromTicketCreationDto(TicketCreationDto creationDto)
    {
        return mapper.Map<Ticket>(creationDto);
    }

    public Ticket FromTicketResponseDto(TicketResponseDto responseDto)
    {
        return mapper.Map<Ticket>(responseDto);
    }

    public TicketResponseDto FromTicket(Ticket ticket)
    {
        return mapper.Map<TicketResponseDto>(ticket);
    }
    public IEnumerable<TicketResponseDto> FromTicketList(IEnumerable<Ticket> ticket)
    {
        return mapper.Map<IEnumerable<TicketResponseDto>>(ticket);
    }
}