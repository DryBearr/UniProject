using Xunit;
using Repository.Models;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Repository;

namespace RepositoryTest;
public class GenericRepositoryTest
{
    private readonly DbContextOptions<AppDbContext> _options;

    public GenericRepositoryTest()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "GenericRepositoryTestDataBase")
            .Options;
    }

    [Fact]
    public async Task AddAsync_User_ShouldAddUser()
    {
        using (var context = new AppDbContext(_options))
        {
            var repository = new GenericRepository<User>(context);
            var user = new User { UserId = 1, Username = "User1", PasswordHash = "1234" };
            
            await repository.AddAsync(user);

            var addedUser = await context.Users.FirstOrDefaultAsync(u => u.UserId == 1);
            Assert.NotNull(addedUser);
            Assert.Equal("User1", addedUser.Username);
        }
    }

    [Fact]
    public async Task GetByIdAsync_ExistingUserId_ShouldReturnUser()
    {
        using (var context = new AppDbContext(_options))
        {
            var user = new User { UserId = 2, Username = "User2", PasswordHash = "abcd" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var repository = new GenericRepository<User>(context);
            var foundUser = await repository.GetByIdAsync(2);

            Assert.NotNull(foundUser);
            Assert.Equal("User2", foundUser.Username);
        }
    }

    [Fact]
    public async Task UpdateAsync_ExistingUser_ShouldUpdateUser()
    {
        using (var context = new AppDbContext(_options))
        {
            var repository = new GenericRepository<User>(context);
            var user = new User { UserId = 3, Username = "User3", PasswordHash = "xyz" };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            user.Username = "UpdatedUser3";
            await repository.UpdateAsync(user);

            var updatedUser = await context.Users.FirstOrDefaultAsync(u => u.UserId == 3);
            Assert.NotNull(updatedUser);
            Assert.Equal("UpdatedUser3", updatedUser.Username);
        }
    }

    [Fact]
    public async Task DeleteAsync_ExistingUserId_ShouldDeleteUser()
    {
        using (var context = new AppDbContext(_options))
        {
            var repository = new GenericRepository<User>(context);
            var user = new User { UserId = 4, Username = "User4", PasswordHash = "delete" };
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();

            await repository.DeleteAsync(4);

            var deletedUser = await context.Users.FirstOrDefaultAsync(u => u.UserId == 4);
            Assert.Null(deletedUser);
        }
    }

}
