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
        
    }

}