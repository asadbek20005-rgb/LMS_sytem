using LMS.Common.Constants;
using Microsoft.Extensions.Caching.Memory;

namespace LMS.Service.MemoryCache
{
    public class MemoryCacheService(IMemoryCache memoryCache)
    {
        private readonly IMemoryCache _memoryCache = memoryCache;

        public void SetPhoneNumberCode(string phonenumber, int code)
        {
            _memoryCache.Set(phonenumber, code, TimeSpan.FromMinutes(Constants.CodeExpiryTime));
        }

        public void SetUsernameCode(string username, int code)
        {
            _memoryCache.Set(username, code, TimeSpan.FromMinutes(Constants.CodeExpiryTime));
        }

        public object GetPhoneNumberCode(string phoneNumber)
        {
            var value = _memoryCache.Get(phoneNumber);
            if (value == null)
                throw new Exception("Enter the regsterd phone number");
            return value;   
        }

        public void SetEntity(string key,object entity)
        {
            _memoryCache.Set(key: key, entity, TimeSpan.FromMinutes(Constants.CodeExpiryTime));
        }

        public object? GetEntity(string key)
        {
            if (_memoryCache.TryGetValue(key, out object? value)) { return value; }
            return null;
        }



    }
}
