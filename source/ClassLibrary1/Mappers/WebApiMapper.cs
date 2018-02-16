using ClassLibrary1.Model;
using System.Collections.Generic;

namespace ClassLibrary1.Mappers
{
    class WebApiMapper
    {
        public WebApi Map(IDictionary<string, object> model)
        {
            var name = model["name"] as string;
            var description = model["description"] as string;
            var host = model["host"] as string;
            IEnumerable<string> schemes = StringEnumerationMapper.Map(model["schemes"] as object[]);
            IEnumerable<EndPoint> endPoints = new EndPointMapper().Map(model["endpoints"] as object[]);
            string basePath = model["basePath"] as string;
            IEnumerable<string> accepts = StringEnumerationMapper.Map(model["accepts"] as object[]);
            IEnumerable<string> contentType = StringEnumerationMapper.Map(model["contentType"] as object[]);
            string version = model["version"] as string;
            string termsOfService = model["termsOfService"] as string;
            Organization provider = null;
            License license = null;
            IEnumerable<Documentation> documentations = null;
            IEnumerable<Parameter> baseUriParameters = null;
            IEnumerable<ParametrizedSecurityScheme> security = null;
            return new WebApi(name, description, host, schemes, endPoints, basePath, accepts, contentType, version, termsOfService, 
                provider, license, documentations, baseUriParameters, security);
        }

    }
}
