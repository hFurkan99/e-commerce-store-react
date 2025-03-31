using App.Application.Contracts.Persistence;
using App.Domain.Entities;

namespace App.Persistence.Products;

public class ProductRepository(AppDbContext context) : GenericRepository<Product, long>(context), IProductRepository
{
    //public async Task<IEnumerable<Product>> GetTopPriceProductsAsync(int count)
    //{
    //    return await Context.Products
    //        .AsNoTracking()
    //        .OrderByDescending(x => x.Price)
    //        .Take(count)
    //        .ToListAsync();
    //}
}
