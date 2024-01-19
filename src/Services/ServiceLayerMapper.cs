namespace Services;
using AutoMapper;
using Repository.Models;

public class ServiceLayerMapper : Profile
{
    public ServiceLayerMapper()
    {
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.UnhashedPassword))
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
        CreateMap<User, GetUserDto>();
        
        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.UnhashedPassword));

        CreateMap<Post, PostDto>();
        CreateMap<Post, CreatePostDto>();

        CreateMap<Comment, CommentDto>();
        CreateMap<Comment, ReplyCommentDto>();
    }
}