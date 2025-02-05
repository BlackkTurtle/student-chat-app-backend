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
    public class FileBaseConfiguration : IEntityTypeConfiguration<FileBase>
    {
        public void Configure(EntityTypeBuilder<FileBase> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.BlobName).HasMaxLength(100).IsRequired();

            builder.Property(x => x.MimeType).HasMaxLength(10).IsRequired();

            builder.HasMany(c => c.AppUsers)
                .WithOne(c => c.FileBase)
                .HasForeignKey(c => c.Avatar);

            builder.HasMany(c => c.Groups)
                .WithOne(c => c.FileBase)
                .HasForeignKey(c => c.ImageId);

            builder.HasMany(x => x.Messages)
                .WithMany(x => x.FileBases);
        }
    }
}
