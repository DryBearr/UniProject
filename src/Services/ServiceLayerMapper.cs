namespace Services;
using AutoMapper;
using Repository.Models;

public class ServiceLayerMapper : Profile
{
    public ServiceLayerMapper()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, GetUserDto>();
        CreateMap<User, UpdateUserDto>();

        CreateMap<Post, PostDto>();
        CreateMap<Post, CreatePostDto>();

        CreateMap<Comment, CommentDto>();
        CreateMap<Comment, ReplyCommentDto>();
    }
}