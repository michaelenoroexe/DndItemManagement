using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Configuration;

internal class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasColumnName("RoomID");
        builder.Property(x => x.Name).HasColumnName("RoomName").HasMaxLength(20).IsRequired(true);
        builder.Property(x => x.Password).HasColumnName("RoomPassword").HasMaxLength(25).IsRequired(true);
        builder.Property(x => x.Started).HasColumnName("Started").IsRequired(true).HasDefaultValue(false);
        builder.HasCheckConstraint("RoomPassword", " Len(DmPassword) > 4");

        builder.HasOne(r => r.DM).WithMany(d => d.Rooms).HasForeignKey(r => r.DmId).IsRequired(true);
    }
}
