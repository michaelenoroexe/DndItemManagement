using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

internal class ActionConfiguration : IEntityTypeConfiguration<Entities.Models.Action>
{
    public void Configure(EntityTypeBuilder<Entities.Models.Action> builder)
    {
        builder.ToTable("Action");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("ActionID");
        builder.Property(x => x.Name).HasColumnName("Act").HasMaxLength(50).IsRequired(true);
        builder.HasIndex(x => x.Name).IsUnique(true);
    }
}


