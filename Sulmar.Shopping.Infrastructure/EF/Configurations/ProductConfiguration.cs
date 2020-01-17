using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sulmar.Shopping.Domain;

namespace Sulmar.Shopping.Infrastructure.EF.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
               .Property(p => p.Name)
               .HasMaxLength(200)
               .IsRequired();

        }
    }
}
