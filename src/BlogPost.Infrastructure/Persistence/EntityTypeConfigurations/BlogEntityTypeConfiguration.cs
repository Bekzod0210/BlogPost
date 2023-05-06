using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogPost.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPost.Infrastructure.Persistence.EntityTypeConfigurations
{
    public class BlogEntityTypeConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Blogs)
                .HasForeignKey(p => p.UserId);
        }
    }
}
