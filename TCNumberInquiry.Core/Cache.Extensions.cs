using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using TCNumberInquiry.Core.Entitiy;

namespace TCNumberInquiry.Core
{
	public partial class Extensions
	{
		private class CacheEntry<T>
		{
			public long UpdatedOn { get; set; }

			public T Data { get; set; }

			public CacheEntry()
			{
				this.UpdatedOn = DateTime.UtcNow.Ticks;
			}

			public CacheEntry(T data) : this()
			{
				this.Data = data;
			}
		}

		private static readonly Dictionary<CacheExpiryStrategy, Action<DistributedCacheEntryOptions>> CacheStrategies = new Dictionary<CacheExpiryStrategy, Action<DistributedCacheEntryOptions>>()
		{
			{ CacheExpiryStrategy.Default, (x) => x.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) },
			{ CacheExpiryStrategy.FavorWrite, (x) => x.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(20) },
			{ CacheExpiryStrategy.Balanced, (x) => x.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60) },
			{ CacheExpiryStrategy.FavorRead, (x) => x.SlidingExpiration = TimeSpan.FromMinutes(60) },
		};

		private static string GetSafeString(this IDistributedCache cache, string key)
		{
			if (Extensions.CacheDisabled) return null;
			return cache.GetString(key);
		}

		private static async Task<string> GetSafeStringAsync(this IDistributedCache cache, string key)
		{
			if (Extensions.CacheDisabled) return null;
			return await cache.GetStringAsync(key);
		}

		private static void SetSafeString(this IDistributedCache cache, string key, string value, DistributedCacheEntryOptions options)
		{
			if (Extensions.CacheDisabled) return;
			cache.SetString(key, value, options);
		}

		private static async Task SetSafeStringAsync(this IDistributedCache cache, string key, string value, DistributedCacheEntryOptions options)
		{
			if (Extensions.CacheDisabled) return;
			await cache.SetStringAsync(key, value, options);
		}

		private static void SafeRemove(this IDistributedCache cache, string key)
		{
			if (Extensions.CacheDisabled) return;
			cache.Remove(key);
		}

		public static T GetOrSet<T>(this IDistributedCache cache, string key, CacheExpiryStrategy strategy, Func<T> factory) where T : class
		{
			var cacheEntry = cache.GetOrSet<T>(key, (options) =>
			{
				Extensions.CacheStrategies[strategy].Invoke(options);
				T cachedItem = factory.Invoke();
				return cachedItem;
			});

			return cacheEntry?.Data;
		}

		public static T GetOrSet<T>(this IDistributedCache cache, string key, string dependencyKey, CacheExpiryStrategy strategy, Func<T> factory)
		{
			string dependencyValue = cache.GetSafeString(dependencyKey);

			if (!long.TryParse(dependencyValue, out long dependencyTimestamp))
			{
				cache.SafeRemove(key);
				dependencyTimestamp = 0;
			}

			var cacheEntry = cache.GetOrSet<T>(key, (options) =>
			{
				Extensions.CacheStrategies[strategy].Invoke(options);
				T cachedItem = factory.Invoke();

				cache.SetSafeString(dependencyKey, Convert.ToString(DateTime.UtcNow.Ticks), new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(24) });
				return cachedItem;
			});

			if (0 < dependencyTimestamp && cacheEntry.UpdatedOn < dependencyTimestamp)
			{
				cacheEntry = cache.Set(key, (options) =>
				{
					Extensions.CacheStrategies[strategy].Invoke(options);
					return factory.Invoke();
				});
			}

			return cacheEntry.Data;
		}

		public static async Task<T> GetOrSetAsync<T>(this IDistributedCache cache, string key, CacheExpiryStrategy strategy, Func<Task<T>> factory) where T : class
		{
			CacheEntry<T> cacheEntry = await cache.GetOrSetAsync<T>(key, async (options) =>
			{
				Extensions.CacheStrategies[strategy].Invoke(options);
				T cachedItem = await factory.Invoke();
				return cachedItem;
			});

			return cacheEntry?.Data;
		}

		public static async Task<T> GetOrSetAsync<T>(this IDistributedCache cache, string key, string dependencyKey, CacheExpiryStrategy strategy, Func<Task<T>> factory)
		{
			string dependencyValue = cache.GetSafeString(dependencyKey);

			if (!long.TryParse(dependencyValue, out long dependencyTimestamp))
			{
				cache.SafeRemove(key);
				dependencyTimestamp = 0;
			}

			var cacheEntry = await cache.GetOrSetAsync<T>(key, async (options) =>
			{
				Extensions.CacheStrategies[strategy].Invoke(options);
				T cachedItem = await factory.Invoke();

				await cache.SetSafeStringAsync(dependencyKey, Convert.ToString(DateTime.UtcNow.Ticks), new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(24) });
				return cachedItem;
			});

			if (0 < dependencyTimestamp && cacheEntry.UpdatedOn < dependencyTimestamp)
			{
				cacheEntry = await cache.SetAsync(key, async (options) =>
				{
					Extensions.CacheStrategies[strategy].Invoke(options);
					return await factory.Invoke();
				});
			}

			return cacheEntry.Data;
		}

		public static void NotifyChanged(this IDistributedCache cache, string dependencyKey)
		{
			if (string.IsNullOrWhiteSpace(dependencyKey)) return;

			cache.SetSafeString(dependencyKey, Convert.ToString(DateTime.UtcNow.Ticks), new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(24) });
		}

		public static async Task NotifyChangedAsync(this IDistributedCache cache, string dependencyKey)
		{
			if (string.IsNullOrWhiteSpace(dependencyKey)) return;

			await cache.SetSafeStringAsync(dependencyKey, Convert.ToString(DateTime.UtcNow.Ticks), new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(24) });
		}

		private static CacheEntry<T> GetOrSet<T>(this IDistributedCache cache, string key, Func<DistributedCacheEntryOptions, T> factory)
		{
			string buffer = cache.GetSafeString(key);

			if (null != buffer)
			{
				return Utilities.SafeDeserialize<CacheEntry<T>>(buffer);
			}

			return cache.Set(key, factory);
		}

		private static CacheEntry<T> Set<T>(this IDistributedCache cache, string key, Func<DistributedCacheEntryOptions, T> factory)
		{
			DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
			T item = factory.Invoke(options);
			CacheEntry<T> cacheEntry = new CacheEntry<T>(item);
			string buffer = Utilities.SafeSerialize(cacheEntry);
			cache.SetSafeString(key, buffer, options);
			return cacheEntry;
		}

		private static async Task<CacheEntry<T>> GetOrSetAsync<T>(this IDistributedCache cache, string key, Func<DistributedCacheEntryOptions, Task<T>> factory)
		{
			string buffer = await cache.GetSafeStringAsync(key);

			if (null != buffer)
			{
				return Utilities.SafeDeserialize<CacheEntry<T>>(buffer);
			}

			return await cache.SetAsync(key, factory);
		}

		private static async Task<CacheEntry<T>> SetAsync<T>(this IDistributedCache cache, string key, Func<DistributedCacheEntryOptions, Task<T>> factory)
		{
			DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
			T item = await factory.Invoke(options);
			CacheEntry<T> cacheEntry = new CacheEntry<T>(item);
			string buffer = Utilities.SafeSerialize(cacheEntry);
			await cache.SetSafeStringAsync(key, buffer, options);
			return cacheEntry;
		}
	}
}
