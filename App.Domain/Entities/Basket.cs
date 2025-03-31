using App.Domain.Entities.Common;

namespace App.Domain.Entities;

public class Basket : BaseEntity<long>
{
    public List<BasketItem> Items { get; set; } = default!;
}
