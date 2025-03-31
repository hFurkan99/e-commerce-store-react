using App.Domain.Entities;

namespace App.Application.Contracts.Persistence;

public interface IBasketRepository : IGenericRepository<Basket, long>
{
    Task<Basket?> GetBasketByIdAsync(long id);
}
