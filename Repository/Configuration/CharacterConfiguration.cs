using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

internal class CharacterConfiguration : IEntityTypeConfiguration<Character>
{
    public void Configure(EntityTypeBuilder<Character> builder)
    {
        builder.ToTable("Characters");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("CharacterID");
        builder.Property(x => x.Name).HasColumnName("CharacterName").HasMaxLength(30).IsRequired(true);
        builder.Property(x => x.Currency).HasColumnName("Currency").IsRequired(true).HasDefaultValue(0);

        builder.HasOne(c => c.Room).WithMany(r => r.Characters).HasForeignKey(r => r.RoomId).IsRequired(true);
    }
}
