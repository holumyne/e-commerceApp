using Core.Entities;
using Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;

        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase(); //with this we hv got connection to our Redis db.
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);//our basket will be stored as string in our Redis db.

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);//if we hv a data then will deserialize it to CustomerBasket otherwise, will return null
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await _database.StringSetAsync(basket.Id, JsonSerializer.Serialize(basket),
                TimeSpan.FromDays(30));//the time to live.i.e the period to store the info in our basket in our server

            if (!created)
                return null;

            return await GetBasketAsync(basket.Id);
        }
    }
}
