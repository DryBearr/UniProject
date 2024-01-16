namespace Services;
using Repository;
using Repository.Repositories;
using Repository.Models;


public interface IUserService 
{
    Task<UserDto> GetUserById(int id);
    Task<UserDto> GetUserByUsername(string username);
    Task CreateUser(UserDto user);
    Task DeleteUserById(int id);
    Task UpdataUser (UpdateUserDto user);
}



 