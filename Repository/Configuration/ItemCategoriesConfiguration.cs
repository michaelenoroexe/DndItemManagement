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
    }
}
