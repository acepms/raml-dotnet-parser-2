using System.Collections.Generic;

namespace ClassLibrary1.Model
{
    public class AnyShape : Shape
    {
        /// <summary>
        /// AnyShape
        /// </summary>
        public AnyShape(Documentation documentation, XmlSerializer xmlSerialization, IEnumerable<Example> examples,
            string name, string displayName, string description, string @default, IEnumerable<string> values, IEnumerable<Shape> inherits)
            : base(name, displayName, description, @default, values, inherits)
        {
            Documentation = documentation;
            XmlSerialization = xmlSerialization;
            Examples = examples;
        }
        // any
        public Documentation Documentation { get; }
        public XmlSerializer XmlSerialization { get; }
        public IEnumerable<Example> Examples { get; }

    }
}