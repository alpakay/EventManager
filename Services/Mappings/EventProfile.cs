using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace Services.Mappings;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<EventFormDto, Event>();
        CreateMap<Event, EventFormDto>()
            .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator != null ? src.Creator.FullName : ""));
        CreateMap<Event, EventShowDto>()
            .ForMember(dest => dest.CreatorName, opt => opt.MapFrom(src => src.Creator != null ? src.Creator.FullName : ""))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StartDate > DateTime.Now ? "Aktif" : "Geçmiş"));
    }
}