using App.Application.Features.Products.Dto;

namespace App.Application.Features.Baskets.Dto;
public record BasketItemDto(long Id, int Quantity, ProductDto Product);
