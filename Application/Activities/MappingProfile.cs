using Application.Activities;
using AutoMapper;
using Domain;
using System.Linq;

namespace Application.Activitites
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Activity, ActivityDto>();
            CreateMap<UserActivity, AttendeeDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(s => s.AppUser.UserName))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(s => s.AppUser.DisplayName))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(s => s.AppUser.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}
