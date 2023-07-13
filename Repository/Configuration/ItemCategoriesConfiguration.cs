using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

internal class ItemCategoriesConfiguration : IEntityTypeConfiguration<ItemCategory>
{
    public void Configure(EntityTypeBuilder<ItemCategory> builder)
    {
        builder.ToTable("ItemCategories");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("CategoryID");
        builder.Property(x => x.Name).HasColumnName("CategoryName").HasMaxLength(150).IsRequired(true);
        builder.HasIndex(x => x.Name).IsUnique(true);

        builder.HasData(new[] {
            new ItemCategory { Id = 1, Name = "ARMOR"},
            new ItemCategory { Id = 2, Name = "POTION"},
            new ItemCategory { Id = 3, Name = "RING"},
            new ItemCategory { Id = 4, Name = "ROD"},
            new ItemCategory { Id = 5, Name = "SCROLL"},
            new ItemCategory { Id = 6, Name = "STAFF"},
            new ItemCategory { Id = 7, Name = "WAND"},
            new ItemCategory { Id = 8, Name = "WEAPON"},
            new ItemCategory { Id = 9, Name = "WONDROUS"},
            new ItemCategory { Id = 10, Name = "OTHER GEAR"}
        });
    }
}
