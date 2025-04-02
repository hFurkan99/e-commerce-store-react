using App.Application.Contracts.Persistence;
using App.Application.Features.Products.Get;
using App.Domain.Entities;
using App.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Products;

public class ProductRepository(AppDbContext context) : GenericRepository<Product, long>(context), IProductRepository
{
    public async Task<GetProductFiltersResponse> GetProductFiltersAsync()
    {
        var brands = await Context.Products.Select(x => x.Brand).Distinct().ToListAsync();
        var types = await Context.Products.Select(x => x.Type).Distinct().ToListAsync();

        var productFilters = new GetProductFiltersResponse(brands, types);

        return productFilters;
    }

    public async Task<PagedList<Product>> GetProductsAsync(GetProductsRequest request)
    {
        List<string> brandList = [];
        List<string> typeList = [];

        var query = Context.Products.AsNoTracking();

        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            var lowerCaseSearchTerm = request.SearchTerm.ToLower();
            query = query.Where(x => x.Name.ToLower().Contains(lowerCaseSearchTerm));
        }

        if (!string.IsNullOrEmpty(request.Brands))
        {
            brandList.AddRange([.. request.Brands.ToLower().Split(',')]);
            query = query.Where(x => brandList.Contains(x.Brand.ToLower()));
        }

        if (!string.IsNullOrEmpty(request.Types))
        {
            typeList.AddRange([.. request.Types.ToLower().Split(',')]);
            query = query.Where(x => typeList.Contains(x.Type.ToLower()));
        }

        if (!string.IsNullOrEmpty(request.OrderBy))
        {
            query = request.OrderBy.ToLower() switch
            {
                "name" => request.Descending ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name),
                "price" => query.OrderBy(x => x.Price),
                "pricedesc" => query.OrderByDescending(x => x.Price),
                "createdate" => request.Descending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
                _ => throw new ArgumentException($"Invalid order by field: {request.OrderBy}"),
            };
        }
        else query = query.OrderBy(x => x.Id);
        
        var products = await ToPagedList(query, request.PageNumber, request.PageSize);

        return products;
    }
}
