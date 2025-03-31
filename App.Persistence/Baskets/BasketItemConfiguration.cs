using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.Baskets;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProductId).IsRequired();
        builder.Property(x => x.Quantity).IsRequired();

        builder.HasOne(x => x.Product)
       .WithMany(x => x.BasketItems)
       .HasForeignKey(x => x.ProductId)
       .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Basket)
       .WithMany(x => x.Items)
       .HasForeignKey(x => x.BasketId)
       .OnDelete(DeleteBehavior.Cascade);
    }
}
