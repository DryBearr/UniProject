using Microsoft.EntityFrameworkCore;
using Xunit;
using Repository.Repositories;
using Repository.Models;
using System;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace RepositoryTest;


public class PostRepositoryTest 
{
    private readonly DbContextOptions<AppDbContext> _options;

    public PostRepositoryTest()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "PostRepositoryTestDatabase")
            .Options;
    }

    [Fact]
    public async Task GetPostsByUserIdTest_ShouldGetPosts_By_UserID()
    {
        int testNumber = 10;
        using (var context = new AppDbContext(_options))
        {
            
            var posts = new List<Post>();
            for (int i = 1; i <= testNumber; i ++)
            {
                posts.Add(new Post {
                        PostId = i,
                        Content = $"Post{i}",
                        UserId = i,
                        DateCreated = DateTime.UtcNow,
                    }); 
                
            }
            
            await context.Posts.AddRangeAsync(posts);
            await context.SaveChangesAsync();
            
            var repository = new PostRepository(context);
            var retrivedPosts = new List<Post>();

            for(int i = 1; i <= testNumber; i ++)
            {
                foreach(var item in await repository.GetPostsByUserIdAsync(i))
                    retrivedPosts.Add(item);
            }

            Assert.Equal(testNumber, retrivedPosts.Count());

            int index = 1;
            foreach(Post post in retrivedPosts)
            {
                Assert.Equal(post.UserId, index);
                index ++;
            }

        }
    }
}