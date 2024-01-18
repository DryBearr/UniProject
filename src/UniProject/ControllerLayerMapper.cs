namespace Contollers;
using AutoMapper;
using Services;
using DTOs;

public class ControllerLayerMapper : Profile
{
    public ControllerLayerMapper()
    {
        CreateMap<UserDto, ResponseUserDto>();
        CreateMap<GetUserDto, ResponseUserDto>();
    }
}