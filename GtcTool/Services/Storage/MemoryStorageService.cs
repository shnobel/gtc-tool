namespace GtcTool.Services.Storage
{
    public class MemoryStorageService
    {
        private Dictionary<string, object> _storage = new Dictionary<string, object>();

        public T? Get<T>(string key)
        {
            if (_storage.ContainsKey(key))
            {
                return (T)_storage[key];
            }
            return default;
        }

        public void Save(string key, object value)
        {
            _storage[key] = value;
        }
    }
}
