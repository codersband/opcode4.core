using System;

namespace opcode4.core.Model.Interfaces.Cache
{
    public interface ICacheProvider : IDisposable
    {
        void Add(string key, object value);
        void Add(string key, object value, TimeSpan validFor);
        void Add(string key, object value, DateTime expiredAt);

        void Set(string key, object value, TimeSpan validFor);
        void Set(string key, object value, DateTime expiredAt);

        void Add(string key, object value, string filePath);
        void Add(string key, object value, DateTime expiredAt, string filePath);

        void Touch(string key, DateTime nextExpiration);
        void Touch(string key, TimeSpan nextExpiration);

        T Get<T>(string key);

        object this[string key] { get; set; }

        void Remove(string key);
    }
}
