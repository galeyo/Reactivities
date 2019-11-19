using AutoMapper;
using Domain;
using System.Linq;

namespace Application.Comments
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>()
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(s => s.Author.UserName))
                .ForMember(dest => dest.DisplayName,
                    opt => opt.MapFrom(s => s.Author.DisplayName))
                .ForMember(dest => dest.Image, opt =>
                    opt.MapFrom(s => s.Author.Photos.FirstOrDefault(x => x.IsMain).Url));
        }
    }
}
