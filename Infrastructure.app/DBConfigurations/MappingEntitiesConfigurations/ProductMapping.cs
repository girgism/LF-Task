using Domain.app.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.app.DBConfigurations.MappingEntitiesConfigurations
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("EA_Product");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).HasMaxLength(75);
            builder.Property(x => x.Description).HasMaxLength(150);
            builder.Property(x => x.Price);
        }
    }
}
