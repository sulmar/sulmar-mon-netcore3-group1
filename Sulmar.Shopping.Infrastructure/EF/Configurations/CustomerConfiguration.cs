using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sulmar.Shopping.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sulmar.Shopping.Infrastructure.EF.Configurations
{

    internal class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                  .Property(p => p.FirstName)
                  .HasMaxLength(50)
                  .IsConcurrencyToken();

            builder
                .Property(p => p.LastName)
                .HasMaxLength(50)
                .IsRequired();

            builder
                .Property(p => p.PostCode)
                .ValueGeneratedOnUpdate();

            builder
                .Property(p => p.Version)
                .IsRowVersion()
                .IsConcurrencyToken();

            //builder
            //    .Property(p => p.Gender)
            //    .HasConversion(
            //        v => v.ToString(),
            //        v =>  (Gender) Enum.Parse(typeof(Gender), v)
            //    );

            builder
                 .Property(p => p.Gender)
                 .HasConversion(new EnumToStringConverter<Gender>());


            builder
                .Property(p => p.Login)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();

            builder
                .Property(p => p.HashPassword)
                .HasMaxLength(50)
                .IsRequired();

        }
    }
}
