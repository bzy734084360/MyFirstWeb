using System.Configuration;

namespace BigStarFK.Cache.Factory
{
    /// <summary>
    /// 缓存工厂类
    /// </summary>
    public class CacheFactory
    {
        public static ICache Cache()
        {
            //string cacheType = ConfigurationManager.AppSettings["CacheType"].Trim();
            string cacheType = "Redis";
            switch (cacheType)
            {
                case "Redis":
                    return new Redis.StackExchangeRedisCache();
                //return new Redis.Cache();
                case "WebCache":
                    return new Cache();
                default:
                    return new Cache();
            }
        }
    }
}
