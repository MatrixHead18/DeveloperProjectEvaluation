using DeveloperStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperStore.ORM.Mapping
{
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SalesItem");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(si => si.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(si => si.Discount).HasColumnType("decimal(18,2)");
            builder.Property(si => si.TotalAmount).HasColumnType("decimal(18,2)");
        }
    }
}
