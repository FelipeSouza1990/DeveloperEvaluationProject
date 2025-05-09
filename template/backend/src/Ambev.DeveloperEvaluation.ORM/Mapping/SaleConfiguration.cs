using Ambev.DeveloperEvaluation.Domain.Sales.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mappings
{
    public class SaleMap : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.SaleNumber).IsRequired();
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.CustomerId).IsRequired();
            builder.Property(x => x.CustomerName).IsRequired();
            builder.Property(x => x.BranchId).IsRequired();
            builder.Property(x => x.BranchName).IsRequired();
            builder.Property(x => x.IsCancelled).IsRequired();

            builder.HasMany(x => x.Items)
                   .WithOne()
                   .HasForeignKey(x => x.SaleId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
