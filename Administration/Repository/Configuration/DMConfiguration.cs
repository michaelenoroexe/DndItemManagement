using Administration.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Administration.Repository.Configuration;

internal class DMConfiguration : IEntityTypeConfiguration<DM>
{
    public void Configure(EntityTypeBuilder<DM> builder)
    {
        builder.ToTable("DMs");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("DmID");
        builder.Property(x => x.Login).HasColumnName("DmLogin").HasMaxLength(20).IsRequired(true);
        builder.HasIndex(x => x.Login).IsUnique(true);
        builder.Property(x => x.Password).HasColumnName("DmPassword").HasMaxLength(65).IsRequired(true);
    }
}
