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
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(ma => new { ma.UserId, ma.ToUserId });

            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.Friendships)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.ToAppUser)
                .WithMany(x => x.ToFriendships)
                .HasForeignKey(x => x.ToUserId);
        }
    }
}
