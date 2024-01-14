namespace RepositoryTest; 

using Xunit;
using Moq;
using Repository.Models;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Repository;

public class UserRepositoryTest
{
    private readonly DbContextOptions<AppDbContext> _options;

    public UserRepositoryTest()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "UserRepositoryTestDataBase")
            .Options;
    }
    


    [Fact]
    public async Task GetUserByUsernameAsync_UserExists_ReturnUser()
    {

        using (var context = new AppDbContext(_options))
        {
            var testUser = new User{UserId = 1, Username = "User1", PasswordHash = "1234"};
            await context.Users.AddAsync(testUser);
            await context.SaveChangesAsync();

            var UserRepository = new UserRepository(context);
            User foundUser = await UserRepository.GetByUsernameAsync("User1");

            Assert.NotNull(foundUser);
            Assert.Equal(1, foundUser.UserId);
            Assert.Equal("1234", foundUser.PasswordHash);
        }
    }

}

