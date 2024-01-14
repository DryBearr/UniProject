using Xunit;
using Repository.Models;
using Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Repository;
namespace RepositoryTest;


public class CommentRepositoryTest
{
    [Fact]
    public async Task GetCommentRepliesTest()
    {
        Random random = new Random();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "CommentRepositoryTestDatabase2")
            .Options;

        using(var context = new AppDbContext(options))
        {
            var comments = new Dictionary<int, List<Comment>>();
            var commentsGroupByPostId = new Dictionary<int, int>();

            for(int i = 1; i <= 10; i ++)
            {
                var baseComment = new Comment{
                                    CommentId = i,
                                    Content = $"Comment{i}",
                                    PostId = i,
                                    UserId = i,
                                    DateCreated = DateTime.UtcNow
                                };
                commentsGroupByPostId[i] = i;

                await context.Comments.AddAsync(baseComment);
                await context.SaveChangesAsync();

                comments[i] = new List<Comment>();
            }
                

            for(int i = 11; i <= 1_000; i ++)
            {
                int randomId = random.Next(1, i);
                var reply = new Comment{
                    CommentId = i,
                    Content = $"Reply To Comment[{randomId}]",
                    ParentCommentId = randomId,
                    PostId = commentsGroupByPostId[randomId],
                    UserId = random.Next(1, i),
                };
                commentsGroupByPostId[i] = reply.PostId;
                comments[randomId].Add(reply);
                comments[i] = new List<Comment>();
            }

            foreach(var item in comments.Keys)
            {
                await context.AddRangeAsync(comments[item]);
                await context.SaveChangesAsync();
            }

            
            var commentRepository  = new CommentRepository(context);
            var retrivedComments = new Dictionary<int, List<Comment>>();
            for(int i = 1; i <= 1_000; i ++)
                retrivedComments[i] = (await commentRepository.GetRepliesByCommentIdAsync(i))
                    .OrderBy(c => c.CommentId)
                    .ToList();
            
            
            for(int i = 1; i <= 1_000; i ++)
            {
                Assert.Equal(comments[i].Count(), retrivedComments[i].Count());
                for(int j = 0; j < comments[i].Count(); j ++)
                {
                    Assert.NotNull(retrivedComments[i][j]);
                    Assert.Equal(comments[i][j].DateCreated, retrivedComments[i][j].DateCreated);
                    Assert.Equal(comments[i][j].PostId, retrivedComments[i][j].PostId);
                    Assert.Equal(comments[i][j].CommentId, retrivedComments[i][j].CommentId);
                    
                    if(comments[i][j].ParentComment == null)
                        continue;
                    Assert.Equal(comments[i][j].ParentCommentId, retrivedComments[i][j].ParentCommentId);
                }
            }            


        }
    }


    [Fact]
    public async Task GetCommentByPostIdTest()
    {
        Random random = new Random();
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "CommentRepositoryTestDatabase1")
            .Options;

        using(var context = new AppDbContext(options))
        {
            var comments = new Dictionary<int, List<Comment>>();
            var commentsGroupByPostId = new Dictionary<int, int>();

            for(int i = 1; i <= 10; i ++) 
            {
                var baseComment = new Comment{
                                    CommentId = i,
                                    Content = $"Comment{i}",
                                    PostId = i,
                                    UserId = i,
                                    DateCreated = DateTime.UtcNow
                                };
                commentsGroupByPostId[i] = i;

                await context.Comments.AddAsync(baseComment);
                await context.SaveChangesAsync();

                comments[i] = new List<Comment>();
            }
                

            for(int i = 11; i <= 1_000; i ++)
            {
                int randomId = random.Next(1, i);
                var reply = new Comment{
                    CommentId = i,
                    Content = $"Reply To Comment[{randomId}]",
                    ParentCommentId = randomId,
                    PostId = commentsGroupByPostId[randomId],
                    UserId = random.Next(1, i),
                };
                commentsGroupByPostId[i] = reply.PostId;
                comments[randomId].Add(reply);
                comments[i] = new List<Comment>();
            }

            foreach(var item in comments.Keys)
            {
                await context.AddRangeAsync(comments[item]);
                await context.SaveChangesAsync();
            }

            
            var commentRepository  = new CommentRepository(context);
            var retrivedComments = new Dictionary<int, List<Comment>>();

            for(int i = 1; i <= 10; i ++)
                retrivedComments[i] = (await commentRepository.GetRepliesByCommentIdAsync(i)).ToList();

            for(int i = 1; i <= 10; i ++)
            {
                foreach(var comment in retrivedComments[i])
                    Assert.Equal(i, comment.PostId);
            }
        
        
        }
    }

}