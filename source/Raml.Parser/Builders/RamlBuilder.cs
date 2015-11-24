﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Raml.Parser.Expressions;

namespace Raml.Parser.Builders
{
	public class RamlBuilder
	{
		public RamlDocument Build(IDictionary<string, object> dynamicRaml)
		{
			var doc = new RamlDocument(dynamicRaml);
			doc.BaseUri = dynamicRaml.ContainsKey("baseUri") ? (string) dynamicRaml["baseUri"] : string.Empty;
			doc.Title = (string)dynamicRaml["title"];
			doc.Version = dynamicRaml.ContainsKey("version") ? (string)dynamicRaml["version"] : null;
			doc.MediaType = dynamicRaml.ContainsKey("mediaType") ? (string)dynamicRaml["mediaType"] : null;
			doc.Documentation = GetDocumentation(dynamicRaml);
			doc.BaseUriParameters = ParametersBuilder.GetUriParameters(dynamicRaml, "baseUriParameters");
			doc.SecuredBy = GetSecuredBy(dynamicRaml);
			doc.Method = dynamicRaml.ContainsKey("method") ? (string)dynamicRaml["method"] : null;
			doc.Protocols = ProtocolsBuilder.Get(dynamicRaml);
			doc.SecuritySchemes = GetSecuritySchemes(dynamicRaml, doc.MediaType);
			doc.ResourceTypes = GetResourceTypes(dynamicRaml, doc.MediaType);
			doc.Traits = GetTraits(dynamicRaml, doc.MediaType);
			doc.Schemas = GetSchemas(dynamicRaml);
			doc.Resources = GetResources(dynamicRaml, doc.ResourceTypes, doc.Traits, doc.MediaType);
		    doc.Types = TypeBuilder.Get(dynamicRaml);
			return doc;
		}

		private ICollection<Resource> GetResources(IDictionary<string, object> dynamicRaml, IEnumerable<IDictionary<string, ResourceType>> resourceTypes,
            IEnumerable<IDictionary<string, Method>> traits, string defaultMediaType)
		{
			var resources = new Collection<Resource>();
		    foreach (var key in dynamicRaml.Keys.Where(k => k.StartsWith("/")))
		    {
                var resource = new ResourceBuilder().Build(dynamicRaml, key, resourceTypes, traits, defaultMediaType);
		        resources.Add(resource);
		    }

			return resources;
		}

		private IEnumerable<IDictionary<string, string>> GetSchemas(IDictionary<string, object> dynamicRaml)
		{
			return dynamicRaml.ContainsKey("schemas")
					? ((object[])dynamicRaml["schemas"])
						.Cast<IDictionary<string, object>>()
						.Select(o => o.ToDictionary(kv => kv.Key, kv => (string)kv.Value))
						.ToArray()
					: new Dictionary<string, string>[0];
		}

		private IEnumerable<IDictionary<string, SecurityScheme>> GetSecuritySchemes(IDictionary<string, object> dynamicRaml,string defaultMediaType)
		{
			if (!dynamicRaml.ContainsKey("securitySchemes"))
				return new List<IDictionary<string, SecurityScheme>>();

			var schemes = (object[])dynamicRaml["securitySchemes"];

			return (from IDictionary<string, object> scheme in schemes
					select scheme
                    .ToDictionary(kv => kv.Key, kv => new SecuritySchemeBuilder().Build((IDictionary<string, object>)kv.Value, defaultMediaType)))
				.Cast<IDictionary<string, SecurityScheme>>()
				.ToList();
		}

		private static IEnumerable<DocumentationItem> GetDocumentation(IDictionary<string, object> dynamicRaml)
		{
			return dynamicRaml.ContainsKey("documentation")
				? ((object[]) dynamicRaml["documentation"]).Select(i => new DocumentationItemBuilder().Build((IDictionary<string, object>) i))
				: new List<DocumentationItem>();
		}

		private static IEnumerable<string> GetSecuredBy(IDictionary<string, object> dynamicRaml)
		{
			object value;
			var securedBy = new string[0];
			if (dynamicRaml.TryGetValue("securedBy", out value))
				securedBy = ((Object[]) value).Cast<string>().ToArray();
			return securedBy;
		}

        private IEnumerable<IDictionary<string, ResourceType>> GetResourceTypes(IDictionary<string, object> dynamicRaml, string defaultMediaType)
		{
			if (!dynamicRaml.ContainsKey("resourceTypes"))
				return new List<IDictionary<string, ResourceType>>();

			var dynamicResourceTypes = (object[])dynamicRaml["resourceTypes"];
			return (from IDictionary<string, object> dynamicResourceType in dynamicResourceTypes
					select dynamicResourceType
                        .ToDictionary(kv => kv.Key, kv => new ResourceTypeBuilder().Build((IDictionary<string, object>)kv.Value, defaultMediaType)))
				.Cast<IDictionary<string, ResourceType>>()
				.ToList();
		}

        private IEnumerable<IDictionary<string, Method>> GetTraits(IDictionary<string, object> dynamicRaml, string defaultMediaType)
		{
			if (!dynamicRaml.ContainsKey("traits"))
				return new List<IDictionary<string, Method>>();

			var dynamicTraits = ((object[])dynamicRaml["traits"]).Cast<IDictionary<string, object>>();
			return dynamicTraits
				.Select(dyn => dyn
                    .ToDictionary(kv => kv.Key, kv => new MethodBuilder().Build((IDictionary<string, object>)kv.Value, defaultMediaType)))
				.ToArray();
		}


	}
}