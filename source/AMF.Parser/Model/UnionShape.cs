using System.Collections.Generic;

namespace ClassLibrary1.Model
{
    public class UnionShape : AnyShape
    {
        /// <summary>
        /// Union
        /// </summary>
        public UnionShape(IEnumerable<Shape> anyOf, Documentation documentation, XmlSerializer xmlSerialization, IEnumerable<Example> examples,
            string name, string displayName, string description, string @default, IEnumerable<string> values, IEnumerable<Shape> inherits)
            : base(documentation, xmlSerialization, examples, name, displayName, description, @default, values, inherits)
        {
            AnyOf = anyOf;
        }

        // union
        public IEnumerable<Shape> AnyOf { get; }
    }
}