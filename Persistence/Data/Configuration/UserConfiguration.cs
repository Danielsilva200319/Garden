using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id);

            builder.Property(x => x.UserName).IsRequired().HasMaxLength(50);

            builder.Property(x => x.Password).IsRequired().HasMaxLength(225);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);

            builder.HasMany(e => e.Rols).WithMany(c => c.Users).UsingEntity<UserRol>(
                y => y.HasOne(e => e.Rols).WithMany(e => e.UsersRols).HasForeignKey(c => c.IdRol),
                y => y.HasOne(e => e.Users).WithMany(e => e.UserRols).HasForeignKey(c => c.IdUser),
                y =>
                {
                    y.ToTable("userrol");
                    y.HasKey(z => new { z.IdUser, z.IdRol });
                }
            );
        }
    }
}