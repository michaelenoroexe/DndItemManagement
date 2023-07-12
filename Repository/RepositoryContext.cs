using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : DbContext  
    {
        public DbSet<Entities.Models.Action>? Actions { get; set; }
        public DbSet<Character>? Characters { get; set; }
        public DbSet<CharacterItem>? CharactersItems { get; set; }
        public DbSet<ItemCategory>? Items { get; set; }
        public DbSet<ItemCategory>? ItemCategories { get; set; }

        public RepositoryContext(DbContextOptions options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ActionConfiguration());
            modelBuilder.ApplyConfiguration(new CharacterConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new ItemCategoriesConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
