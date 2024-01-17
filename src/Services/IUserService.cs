namespace Services;
using Repository;
using Repository.Repositories;
using Repository.Models;


public interface IUserService 
{
    Task<GetUserDto> GetUserById(int id);
    Task<GetUserDto> GetUserByUsername(string username);
    Task<IEnumerable<GetUserDto>> GetAllUsers();
    Task CreateUser(UserDto user);
    Task DeleteUserById(int id);
    Task UpdataUser (UpdateUserDto user);
}



 