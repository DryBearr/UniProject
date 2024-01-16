using Microsoft.EntityFrameworkCore;
using Repository.Models;
namespace Repository;



public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    //_______________Db Sets_______________
    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts  {get; set;}
    public DbSet<Comment> Comments  {get; set;}
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasMany(c => c.Replies)
            .WithOne(r => r.ParentComment) 
            .HasForeignKey(c => c.ParentCommentId)
            .IsRequired(false) 
            .OnDelete(DeleteBehavior.Cascade);
    }

}