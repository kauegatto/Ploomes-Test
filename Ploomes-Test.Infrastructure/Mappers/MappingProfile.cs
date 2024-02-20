using AutoMapper;
using Ploomes_Test.Domain;
using Ploomes_Test.Domain.Dto.Ticket;

namespace Ploomes_Test.Infrastructure.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TicketCreationDto, Ticket>()
            .ConstructUsing( x => new Ticket(x.RequesterEmail, x.Subject, x.Description));
        CreateMap<TicketResponseDto, Ticket>();
        CreateMap<Ticket, TicketResponseDto>();
        CreateMap<IEnumerable<Ticket>, IEnumerable<TicketResponseDto>>();
    }
}