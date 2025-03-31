using App.Application.Features.Baskets.Add;
using App.Application.Features.Baskets.Dto;
using App.Domain.Entities;
using AutoMapper;

namespace App.Application.Features.Baskets;

public class BasketMappingProfile : Profile
{
    public BasketMappingProfile()
    {
        CreateMap<AddItemRequest, BasketItem>();   
        CreateMap<Basket, BasketDto>().ReverseMap();
        CreateMap<BasketItem, BasketItemDto>().ReverseMap();
    }
}
