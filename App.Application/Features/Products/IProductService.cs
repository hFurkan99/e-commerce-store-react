using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Get;
using App.Application.Features.Products.Update;
using App.Domain.Entities.Common;

namespace App.Application.Features.Products;

public interface IProductService
{
    Task<ServiceResult<IEnumerable<ProductDto>>> GetAllAsync();
    Task<ServiceResult<PagedList<ProductDto>>> GetProductsAsync(GetProductsRequest request);
    Task<ServiceResult<GetProductFiltersResponse>> GetProductFiltersAsync();
    Task<ServiceResult<PaginatedResult<ProductDto>>> GetPagedAsync(int pageNumber, int pageSize);
    Task<ServiceResult<ProductDto?>> GetByIdAsync(int id);
    Task<ServiceResult<long>> CreateAsync(CreateProductRequest request);
    Task<ServiceResult> UpdateAsync(UpdateProductRequest request);
    Task<ServiceResult> UpdateStockAsync(int id, int stock);
    Task<ServiceResult> DeleteAsync(int id);
}
