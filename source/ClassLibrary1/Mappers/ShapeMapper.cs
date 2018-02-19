using System.Collections.Generic;
using ClassLibrary1.Model;
using System.Linq;

namespace ClassLibrary1.Mappers
{
    internal class ShapeMapper
    {
        internal static Shape MapSchema(IDictionary<string, object> schema)
        {
            if (schema == null)
                return null;

            return new Shape(schema["name"] as string, schema["displayName"] as string, schema["description"] as string, 
                schema["default"] as string, StringEnumerationMapper.Map(schema["values"] as object[]), 
                Map(schema["inherits"] as object[]));
        }

        internal static Shape Map(IDictionary<string, object> shape)
        {
            if (shape == null)
                return null;

            return new Shape(shape["name"] as string, shape["displayName"] as string, shape["description"] as string, shape["default"] as string,
                StringEnumerationMapper.Map(shape["values"] as object[]), Map(shape["inherits"] as object[]));
        }

        private static IEnumerable<Shape> Map(object[] shapes)
        {
            if (shapes == null)
                return new Shape[0];

            return shapes.Select(s => Map(s as IDictionary<string, object>)).ToArray();
        }
    }
}