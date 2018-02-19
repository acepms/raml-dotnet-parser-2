using System.Collections.Generic;
using ClassLibrary1.Model;
using System.Linq;

namespace ClassLibrary1.Mappers
{
    internal class DocumentationMapper
    {
        internal static IEnumerable<Documentation> Map(object[] documentations)
        {
            if (documentations == null)
                return new Documentation[0];

            return documentations.Select(d => Map(d as IDictionary<string, object>)).ToArray();
        }

        internal static Documentation Map(IDictionary<string, object> doc)
        {
            if (doc == null)
                return null;

            return new Documentation(doc["url"] as string, doc["description"] as string, doc["title"] as string);
        }
    }
}