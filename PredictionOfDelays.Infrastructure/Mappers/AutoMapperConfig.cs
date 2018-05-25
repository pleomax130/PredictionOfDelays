using AutoMapper;
using PredictionOfDelays.Core.Models;
using PredictionOfDelays.Infrastructure.DTO;

namespace PredictionOfDelays.Infrastructure.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ApplicationUser, ApplicationUserDto>();
                    cfg.CreateMap<ApplicationUserDto, ApplicationUser>();
                    cfg.CreateMap<Event, EventDto>();
                    cfg.CreateMap<Group, GroupDto>();
                    cfg.CreateMap<GroupDto, Group>();
                    cfg.CreateMap<EventDto, Event>();
                    cfg.CreateMap<Localization, LocalizationDto>();
                    cfg.CreateMap<EventInvite, EventInviteDto>();
                    cfg.CreateMap<GroupInvite, GroupInviteDto>();
                    cfg.CreateMap<Invites, InvitesDto>();
                    cfg.CreateMap<UserEvent, UserEventDto>();
                })
                .CreateMapper();
    }
}