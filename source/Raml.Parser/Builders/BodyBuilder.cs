﻿using System.Collections.Generic;
using System.Linq;
using Raml.Parser.Expressions;

namespace Raml.Parser.Builders
{
	public class BodyBuilder
	{
		private readonly IDictionary<string, object> dynamicRaml;

		public BodyBuilder(IDictionary<string, object> dynamicRaml)
		{
			this.dynamicRaml = dynamicRaml;
		}

		public IDictionary<string, MimeType> GetAsDictionary(string defaultMediaType)
		{
            if(dynamicRaml == null)
                return new Dictionary<string, MimeType>();

		    if (dynamicRaml.ContainsKey("type"))
		    {
		        return new Dictionary<string, MimeType> {{defaultMediaType, GetMimeType(dynamicRaml)}};
		    }

			return dynamicRaml.ToDictionary(kv => kv.Key, pair =>
				{
					var value = pair.Value as IDictionary<string, object>;
					if(value != null)
						return GetMimeType(value);

					return new MimeType { Schema = (string) pair.Value};
				});
		}

		public IDictionary<string,Parameter> GetParameters(IDictionary<string, object> dictionary)
		{
            if (dynamicRaml == null)
                return new Dictionary<string, Parameter>();

			return dictionary.ToDictionary(kv => kv.Key, kv => (new ParameterBuilder()).Build((IDictionary<string, object>)kv.Value));
		}

		public IEnumerable<MimeType> Get()
		{
            if (dynamicRaml == null)
                return new List<MimeType>();

			return dynamicRaml.Select(pair =>
			                          {
				                          var value = (IDictionary<string, object>)pair.Value;
				                          return GetMimeType(value);
			                          })
									  .ToArray();
		}

		public MimeType GetMimeType(IDictionary<string, object> value)
		{
			if (value == null)
				return null;

			if (value.ContainsKey("body"))
				value = value["body"] as IDictionary<string, object>;

			return new MimeType
			       {
                       Type = value.ContainsKey("type") ? (string)value["type"] : null,
				       Description = value.ContainsKey("description") ? (string) value["description"] : null,
				       Example = value.ContainsKey("example") ? (string) value["example"] : null,
				       Schema = value.ContainsKey("schema") ? (string) value["schema"] : null,
				       FormParameters = value.ContainsKey("formParameters")
					       ? GetParameters((IDictionary<string, object>) value["formParameters"])
					       : null,
			       };
		}
	}
}