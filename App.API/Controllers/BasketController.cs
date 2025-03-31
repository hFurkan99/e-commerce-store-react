using App.Application.Features.Baskets;
using App.Application.Features.Baskets.Add;
using App.Application.Features.Baskets.Remove;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    public class BasketController(IBasketService basketService) : CustomBaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetBasket(long basketId) => CreateActionResult(await basketService.GetBasketAsync(basketId));

        [HttpDelete]
        public async Task<IActionResult> ClearBasket(long basketId) => CreateActionResult(await basketService.ClearBasketAsync(basketId));

        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(AddItemRequest request) => CreateActionResult(await basketService.AddItemAsync(request));

        [HttpDelete]
        public async Task<IActionResult> RemoveItemFromBasket(RemoveItemRequest request) => CreateActionResult(await basketService.RemoveItemAsync(request));
    }
}
