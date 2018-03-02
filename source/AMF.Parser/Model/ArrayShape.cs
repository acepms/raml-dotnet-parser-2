using System.Collections.Generic;

namespace ClassLibrary1.Model
{
    public class ArrayShape : AnyShape
    {
        /// <summary>
        /// ArrayShape
        /// </summary>
        public ArrayShape(Shape items, int minItems, int maxItems, bool uniqueItems, 
            Documentation documentation, XmlSerializer xmlSerialization, IEnumerable<Example> examples,
            string name, string displayName, string description, string @default, IEnumerable<string> values, IEnumerable<Shape> inherits)
            : base(documentation, xmlSerialization, examples, name, displayName, description, @default, values, inherits)
        {
            Items = items;
            MinItems = minItems;
            MaxItems = maxItems;
            UniqueItems = uniqueItems;
        }

        // array
        public int MinItems { get; }
        public int MaxItems { get; }
        public bool UniqueItems { get; }
        public Shape Items { get; }
    }
}