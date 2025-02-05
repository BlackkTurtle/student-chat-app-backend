using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentChat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.DAL.Persistence.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x =>x.Name).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Description).HasMaxLength(500);

            builder.HasMany(x => x.UserGroups)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId);

            builder.HasMany(x => x.Messages)
                .WithOne(x => x.Group)
                .HasForeignKey(x => x.GroupId);

            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.CreatedBy);

            builder.HasOne(x => x.FileBase)
                .WithMany(x => x.Groups)
                .HasForeignKey(x => x.ImageId);
        }
    }
}
