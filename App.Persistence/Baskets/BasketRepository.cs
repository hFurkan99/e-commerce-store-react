using App.Application.Contracts.Persistence;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Baskets;

public class BasketRepository(AppDbContext context) : GenericRepository<Basket, long>(context), IBasketRepository
{
    public async Task<Basket?> GetBasketByIdAsync(long id)
    {
        return await Context.Baskets
            .AsNoTracking()
            .Include(b => b.Items)
            .ThenInclude(bi => bi.Product)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}
