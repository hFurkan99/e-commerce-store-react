using App.Domain.Entities.Common;

namespace App.Application.Features.Products.Get;

public class GetProductsRequest : PaginationParams
{
    public string? SearchTerm { get; set; } = null;
    public string? Brands { get; set; } = null;
    public string? Types { get; set; } = null;
    public string? OrderBy { get; set; } = null;
    public bool Descending { get; set; } = false;
}