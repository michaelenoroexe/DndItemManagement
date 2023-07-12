using Administration.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Administration.Repository.Configuration;

namespace Administration.Repository
{
    public class RepositoryContext : DbContext  
    {
        public DbSet<DM>? DMs { get; set; }
        public DbSet<Room>? Rooms { get; set; }

        public RepositoryContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DMConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
