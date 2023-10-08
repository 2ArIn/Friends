using FriendWithDB.Models;
using Microsoft.EntityFrameworkCore;

namespace FriendsWithDatabase.Data
{
    public class AppDbContext:DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>().HasKey(a=>a.Id);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Friend> Friends { get; set; }
    }
}
