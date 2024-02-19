using AutoMapper;
using Ploomes_Test.Domain;
using Ploomes_Test.Domain.Dto.Ticket;
using Ploomes_Test.Domain.Mappers;

namespace Ploomes_Test.Infrastructure.Mappers;

public class TicketMapper : ITicketMapper
{
    private readonly IMapper mapper;

    public Ticket FromTicketCreationDto(TicketCreationDto creationDto)
    {
        return mapper.Map<Ticket>(creationDto);
    }

    public Ticket FromTicketResponseDto(TicketResponseDto responseDto)
    {
        return mapper.Map<Ticket>(responseDto);
    }
}