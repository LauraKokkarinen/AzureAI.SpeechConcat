using Microsoft.Extensions.Configuration;

namespace AzureAI.Speech.Helpers
{
    public class ConfigurationHelper(IConfiguration configuration)
    {
        private readonly IConfiguration _configuration = configuration;

        public T? GetConfigurationValue<T>(string key, bool mandatory = false)
        {
            var value = _configuration[key];

            if (mandatory && string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(key), $"{key} is null.");

            if (mandatory == false && string.IsNullOrWhiteSpace(value))
                return default;

            if (typeof(T) == typeof(int) || typeof(T) == typeof(int?))
            {
                if (!int.TryParse(value, out int parsedValue))
                    throw new ArgumentException($"{key} is not an integer.", nameof(key));

                return (T)(object)parsedValue;
            }

            if (typeof(T) == typeof(bool) || typeof(T) == typeof(bool?))
            {
                if (!bool.TryParse(value, out bool parsedValue))
                    throw new ArgumentException($"{key} is not a boolean.", nameof(key));

                return (T)(object)parsedValue;
            }

            return (T?)(object?)value;
        }
    }
}
