using System.Collections.Generic;

namespace ClassLibrary1.Mappers
{
    internal class ParameterMapperUtils
    {
        internal static T Map<T>(IDictionary<string, object> dictionary, string key)
        {
            if (!dictionary.ContainsKey(key))
                return default(T);

            return (T)dictionary[key];
        }
    }
}