using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StudentChat.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentChat.DAL.Persistence.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Content).HasMaxLength(500);

            builder.HasMany(x => x.Notifications)
                .WithOne(x => x.Message)
                .HasForeignKey(x => x.MessageId);

            builder.HasMany(x => x.FileBases)
                .WithMany(x => x.Messages);

            builder.HasOne(x => x.Group)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.GroupId);

            builder.HasOne(x => x.AppUser)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.CreatedBy);

            builder.HasOne(x => x.MessageParent)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.MessageId);

            builder.HasOne(x => x.ForwardedMessage)
                .WithMany(x => x.ForwardedMessages)
                .HasForeignKey(x => x.ForwardedMessageId);
        }
    }
}
