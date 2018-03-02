using System.Collections.Generic;
using ClassLibrary1.Model;

namespace ClassLibrary1.Mappers
{
    internal class LicenseMapper
    {
        internal static License Map(IDictionary<string, object> lic)
        {
            if (lic == null)
                return null;

            return new License(lic["url"] as string, lic["name"] as string);
        }
    }
}