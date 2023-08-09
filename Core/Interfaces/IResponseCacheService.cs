namespace Core.Interfaces
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive); //not returning anything.But used to cache any responses from the db and put into the cache db.Here our memory to use here is Redis.
        Task<string> GetCachedResponse(string cacheKey); //remove items from our db
    }
}
