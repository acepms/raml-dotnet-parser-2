using System.Linq;
using System.Collections.Generic;

namespace ClassLibrary1.Mappers
{
    internal static class StringEnumerationMapper
    {
        internal static IEnumerable<string> Map(object[] stringEnumeration)
        {
            return stringEnumeration.Select(a => a as string).ToArray();
        }
    }
}