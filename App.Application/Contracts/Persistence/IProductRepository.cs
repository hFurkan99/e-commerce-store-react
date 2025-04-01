using App.Application.Features.Products.Get;
using App.Domain.Entities;
using App.Domain.Entities.Common;

namespace App.Application.Contracts.Persistence;

public interface IProductRepository : IGenericRepository<Product, long>
{
    Task<PagedList<Product>> GetProductsAsync(GetProductsRequest request);
    Task<GetProductFiltersResponse> GetProductFiltersAsync();
}
