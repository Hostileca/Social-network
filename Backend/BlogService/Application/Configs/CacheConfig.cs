namespace Application.Configs;

public static class CacheConfig
{
    public static readonly TimeSpan BlogCacheTime = TimeSpan.FromMinutes(10);
    public static readonly TimeSpan PostCacheTime = TimeSpan.FromMinutes(30);
}