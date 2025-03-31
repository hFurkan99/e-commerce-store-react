using App.Application.Features.Baskets.Add;
using App.Application.Features.Baskets.Dto;
using App.Application.Features.Baskets.Remove;

namespace App.Application.Features.Baskets;

public interface IBasketService
{
    Task<ServiceResult<BasketDto>> GetBasketAsync(long id);
    Task<ServiceResult> ClearBasketAsync(long id);
    Task<ServiceResult> AddItemAsync(AddItemRequest request);
    Task<ServiceResult> RemoveItemAsync(RemoveItemRequest request);
}
