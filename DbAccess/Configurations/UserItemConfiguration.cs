using DbAccess.Info;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess.Configurations
{
    internal class UserItemConfiguration : IEntityTypeConfiguration<UserItem>
    {
        public void Configure(EntityTypeBuilder<UserItem> builder)
        {
            builder.ToTable("UsersItems");

            builder.HasKey(x => x.Id);

            builder.HasOne(ui => ui.User).WithMany(u => u.Items).HasForeignKey(ui => ui.UserId);
            builder.HasOne(ui => ui.Item).WithMany(i => i.Users).HasForeignKey(ui => ui.ItemId);
        }
    }
    
}
