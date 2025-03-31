using App.Domain.Entities.Common;

namespace App.Domain.Entities;

public class BasketItem : BaseEntity<long>
{
    public int Quantity { get; set; }
    public long ProductId { get; set; }
    public long BasketId { get; set; }
    public Product Product { get; set; } = default!;
    public Basket Basket { get; set; } = default!;
}
