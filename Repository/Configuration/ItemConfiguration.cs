using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

internal class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ItemID");
        builder.Property(x => x.Name).HasColumnName("ItemName").HasMaxLength(50).IsUnicode(true).IsRequired(true);
        builder.Property(x => x.MaxDurability).HasColumnName("ItemMaxDurability").IsRequired(true).HasDefaultValue(1);
        builder.Property(x => x.Price).HasColumnName("Price").IsRequired(true).HasDefaultValue(0);
        builder.Property(x => x.Weight).HasColumnName("Weight").IsRequired(true).HasDefaultValue(0.0f);
        builder.Property(x => x.SecretItemDescription).HasColumnName("SecretItemDescription").HasMaxLength(1000).IsUnicode(true);
        builder.Property(x => x.ItemDescription).HasColumnName("ItemDescription").HasMaxLength(1500);

        builder.HasOne(i => i.ItemCategory).WithMany(ic => ic.Items).HasForeignKey(i => i.ItemCategoryId).IsRequired(true);
        builder.HasOne(i => i.Room).WithMany(r => r.Items).HasForeignKey(i => i.RoomId);

        builder.HasMany(i => i.Actions).WithMany(a => a.Items).UsingEntity("ItemsActions");
        builder.HasMany(i => i.Characters).WithMany(c => c.Items).UsingEntity<CharacterItem>(
            j => j.HasOne(ci => ci.Character).WithMany(c => c.CharacterItems).HasForeignKey(ci => ci.CharacterId),
            j => j.HasOne(ci => ci.Item).WithMany(i => i.CharactersItem).HasForeignKey(ci => ci.ItemId),
            ci =>
            {
                ci.ToTable("CharactersItems");
                ci.Property(j => j.CharacterId).HasColumnName("CharacterID");
                ci.Property(j => j.ItemId).HasColumnName("ItemID");
                ci.Property(j => j.ItemNumber).IsRequired(true).HasDefaultValue(0);
                ci.Property(j => j.CurrentDurability).IsRequired(true).HasDefaultValue(1.0f);
            });
    }
}
