using System.Collections.Generic;

namespace StationNaming.Setting
{
    public class FormatDictionary<TKey> where TKey : notnull
    {
        public NameFormat DefaultFormat { get; set; } = new();

        private readonly Dictionary<TKey, NameFormat> _formats = new();

        public void ApplyFormats(Dictionary<TKey, NameFormat> formats)
        {
            foreach (var pair in formats)
            {
                _formats[pair.Key] = pair.Value;
            }
        }

        public NameFormat GetFormat(TKey source)
        {
            if (!_formats.TryGetValue(source, out var format))
            {
                return DefaultFormat;
            }
            return format.IsValid()
                ? format
                : DefaultFormat;
        }

        public void SetFormat(TKey key, NameFormat format)
        {
            _formats[key] = format;
        }

        public void RemoveFormat(TKey key)
        {
            _formats.Remove(key);
        }

        public NameFormat this[TKey key]
        {
            get => GetFormat(key);
            set => SetFormat(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return _formats.ContainsKey(key);
        }
    }
}