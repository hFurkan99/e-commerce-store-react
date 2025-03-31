namespace App.Application.Features.Baskets.Add;

public record AddItemRequest(long BasketId, long ProductId, int Quantity);