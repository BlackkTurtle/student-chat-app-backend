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
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.FullName).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Description).HasMaxLength(500);

            builder.HasOne(c => c.FileBase)
                .WithMany(c => c.AppUsers)
                .HasForeignKey(c => c.Avatar);

            builder.HasMany(x => x.RefreshTokens)
                .WithOne(c => c.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Specialities)
                .WithMany(c => c.AppUsers);

            builder.HasMany(x => x.Friendships)
                .WithOne(c => c.AppUser)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.ToFriendships)
                .WithOne(c => c.ToAppUser)
                .HasForeignKey(x => x.ToUserId);

            builder.HasMany(x => x.UserGroups)
                .WithOne(c => c.AppUser)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Groups)
                .WithOne(c => c.AppUser)
                .HasForeignKey(x => x.CreatedBy);

            builder.HasMany(x => x.Notifications)
                .WithOne(c => c.AppUser)
                .HasForeignKey(x => x.UserId);

            builder.HasMany(x => x.Messages)
                .WithOne(c => c.AppUser)
                .HasForeignKey(x => x.CreatedBy);
        }
    }
}
