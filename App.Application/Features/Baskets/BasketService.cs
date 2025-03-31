using App.Application.Contracts.Persistence;
using App.Application.Features.Baskets.Add;
using App.Application.Features.Baskets.Dto;
using App.Application.Features.Baskets.Remove;
using App.Domain.Entities;
using AutoMapper;
using System.Net;

namespace App.Application.Features.Baskets;

public class BasketService(
    IBasketRepository basketRepository,
    IGenericRepository<BasketItem, long> basketItemRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IBasketService
{

    public async Task<ServiceResult<BasketDto>> GetBasketAsync(long id)
    {
        Basket? basket = await basketRepository.GetBasketByIdAsync(id);

        if (basket == null)
            return ServiceResult<BasketDto>.Success(null!, HttpStatusCode.OK, "Sepet boş.");

        var basketAsDto = mapper.Map<BasketDto>(basket);

        return ServiceResult<BasketDto>.Success(basketAsDto);
    }

    public async Task<ServiceResult> ClearBasketAsync(long id)
    {
        Basket? basket = await basketRepository.GetByIdAsync(id);

        if (basket == null)
            return ServiceResult.Fail("Sepet yok.");

        basketRepository.Delete(basket);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.OK, "Sepet başarıyla temizlendi.");
    }

    public async Task<ServiceResult> AddItemAsync(AddItemRequest request)
    {
        bool basketExists = await basketRepository.AnyAsync(request.BasketId);
        if (!basketExists) await CreateBasketAsync(request.BasketId);

        BasketItem? existingBasketItem = await ControlProductAndBasketItem(request.ProductId, request.BasketId);

        if (existingBasketItem == null)
        {
            BasketItem basketItem = mapper.Map<BasketItem>(request);
            await basketItemRepository.AddAsync(basketItem);
        }
        else
        {
            existingBasketItem.Quantity += request.Quantity;
            basketItemRepository.Update(existingBasketItem);
        }

        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.Created, "Ürün başarılı şekilde sepete eklendi.");
    }

    public async Task<ServiceResult> RemoveItemAsync(RemoveItemRequest request)
    {
        BasketItem? existingBasketItem = await ControlProductAndBasketItem(request.ProductId, request.BasketId);

        if (existingBasketItem == null)
            return ServiceResult.Fail("İlgili ürün sepette bulunmamaktadır.");

        if (request.Quantity >= existingBasketItem!.Quantity)
            basketItemRepository.Delete(existingBasketItem);
        else
        {
            existingBasketItem.Quantity -= request.Quantity;
            basketItemRepository.Update(existingBasketItem);
        }

        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.OK, "Ürün başarılı şekilde sepetten kaldırıldı.");
    }

    private async Task CreateBasketAsync(long basketId)
    {
        Basket basket = new() { Id = basketId };
        await basketRepository.AddAsync(basket);
    }

    private async Task<BasketItem?> ControlProductAndBasketItem(long productId, long basketId)
    {
        Product? existingProduct = await productRepository.GetByIdAsync(productId) ?? throw new HttpRequestException("Ürün veritabanında bulunmamaktadır.", null, HttpStatusCode.BadRequest);

        BasketItem? existingBasketItem = await basketItemRepository
            .FirstOrDefaultAsync(x => x.BasketId == basketId && x.ProductId == productId);

        return existingBasketItem;
    }
}
