namespace Contollers;
using AutoMapper;
using Services;
public class ControllerLayerMapper : Profile
{
    public ControllerLayerMapper()
    {
        CreateMap<UserDto, ResponseUserDto>();
        CreateMap<GetUserDto, ResponseUserDto>();
    }
}