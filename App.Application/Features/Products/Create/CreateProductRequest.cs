namespace App.Application.Features.Products.Create;

public record CreateProductRequest(
    string Name,
    string Description,
    decimal Price,
    string PictureUrl,
    string Type,
    string Brand,
    int QuantityInStock);