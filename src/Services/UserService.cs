using Repository.Repositories;
using Repository.Models;
using Repository;
using AutoMapper;

namespace Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    public UserService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }
    public async Task CreateUser(UserDto user)
    {
        try
        { 
            if(user == null || user.Username == null || user.UnhashedPassword == null)
                throw new Exception($"user is not valid!");
            if(user.UnhashedPassword.Length < 3)
                throw new Exception($"user has short password");
            if(user.Username.Length < 1)
                throw new Exception($"user has short username");

            User retrievedUser = await _uow.Users.GetByUsernameAsync(user.Username); 
            if(retrievedUser != null)
                throw new Exception($"User with {user.Username} username already exists!");

            var newUser = _mapper.Map<User>(user);
            newUser.PasswordHash = Utils.GetPasswordHash(newUser.PasswordHash);
            
            await _uow.Users.AddAsync(newUser);
            await _uow.SaveAsync();            

        }
        catch(Exception e)
        {
            await _uow.RollBackAsync();
            throw new Exception("Could not Create new User", e);
        }
    }
    public async Task DeleteUserById(int id)
    {
        try
        {
            User user = await _uow.Users.GetByIdAsync(id);
            if(user == null)
                throw new Exception($"Could not delete, User with {id} id does not exist!");

            await _uow.Users.DeleteAsync(user.UserId);
            await _uow.SaveAsync();
        }
        catch(Exception e)
        {
            await _uow.RollBackAsync();

            throw new Exception($"Could not Delete User", e);
        }

    }
    public async Task<GetUserDto> GetUserById(int id)
    {
        try
        {
            User user = await _uow.Users.GetByIdAsync(id);
            if(user == null)
                throw new Exception($"Could not get, User with {id} id does not exist!");

            return _mapper.Map<GetUserDto>(user);
        }
        catch(Exception e)
        {
            throw new Exception($"Could not get User", e);
        }
    }
    public async Task<GetUserDto> GetUserByUsername(string username)
    {
        try
        {
            User user = await _uow.Users.GetByUsernameAsync(username);
            if(user == null)
                throw new Exception($"Could not get, User with {username} username does not exist!");

            return _mapper.Map<GetUserDto>(user);
        }
        catch(Exception e)
        {
            throw new Exception($"Could not get User", e);
        }
    }
    public async Task UpdataUser(UpdateUserDto user)
    {
        try
        { 
            if(user == null || user.Username == null || user.UnhashedPassword == null)
                throw new Exception($"user is not valid!");
            if(user.UnhashedPassword.Length < 3)
                throw new Exception($"user has short password");
            if(user.Username.Length < 1)
                throw new Exception($"user has short username");

            User retrievedUser = await _uow.Users.GetByUsernameAsync(user.Username); 
            if(retrievedUser != null)
                throw new Exception($"User with {user.Username} username already exists!");

            var newUser = _mapper.Map<User>(user);
            newUser.PasswordHash = Utils.GetPasswordHash(newUser.PasswordHash);
            
            await _uow.Users.UpdateAsync(newUser);
            await _uow.SaveAsync();  
        }
        catch(Exception e)
        {
            await _uow.RollBackAsync();
            throw new Exception("Could not update  User", e);
        }
    }
    public async Task<IEnumerable<GetUserDto>> GetAllUsers()
    {
        try
        {
            var users = new List<GetUserDto>();
            var retrievedUsers = await _uow.Users.GetAllAsync();
            
            foreach(var user in retrievedUsers)
                users.Add(_mapper.Map<GetUserDto>(user));

            return users;
        }
        catch(Exception e)
        {
            throw new Exception("Could get all users", e);
        }
    }

}