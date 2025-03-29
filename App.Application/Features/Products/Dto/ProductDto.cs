namespace App.Application.Features.Products.Dto;

public record ProductDto(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string PictureUrl,
    string Type,
    string Brand,
    int QuantityInStock);