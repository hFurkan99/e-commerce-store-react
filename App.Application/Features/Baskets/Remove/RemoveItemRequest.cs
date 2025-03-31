namespace App.Application.Features.Baskets.Remove;

public record RemoveItemRequest(long BasketId, long ProductId, int Quantity);