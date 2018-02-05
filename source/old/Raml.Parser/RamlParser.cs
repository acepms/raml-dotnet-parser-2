using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EdgeJs;
using Raml.Parser.Builders;
using Raml.Parser.Expressions;
using System.IO;
using System.Linq;

namespace Raml.Parser
{
    public class RamlParser
    {
        public async Task<RamlDocument> LoadAsync(string filePath)
        {
            var rawresult = GetDynamicStructureAsync(filePath).Result;

            var ramlDocument = await new RamlBuilder().Build((IDictionary<string, object>)rawresult, filePath);

            return ramlDocument;
        }

        public static async Task<object> GetDynamicStructureAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("filePath");

            var func = Edge.Func(@"
                return function (file, callback) {
                    var parser = require('parser')
                    parser.parse('raml', file, function(model) { return callback(null, model); }, function(error) { return callback(null, error); });
                }
            ");

            var rawresult = await func(filePath);
            return GetRaml(rawresult);
        }

        private static object GetRaml(object rawresult)
        {
            var error = rawresult as string;
            if (!string.IsNullOrWhiteSpace(error) && error.ToLowerInvariant().Contains("error"))
                throw new FormatException(error);

            var ret = rawresult as IDictionary<string, object>;

            HandleErrors(ret);

            return ret["raml"];
        }

        private static void HandleErrors(IDictionary<string, object> ret)
        {
            if (ret == null)
                throw new FormatException("Error while parsing RAML");

            var errorsRaw = ret["errors"];

            var errorObjects = errorsRaw as object[];
            if (errorObjects != null && errorObjects.Length != 0)
            {
                var errorsBuilder = new ErrorsBuilder(errorObjects);
                var errors = errorsBuilder.GetErrors();
                if (errors.Any(e => e.IsWarning == false))
                    throw new FormatException(errorsBuilder.GetMessages());
            }
        }

        public RamlDocument Load(string filePath)
        {
            var rawresult = GetDynamicStructureAsync(filePath).Result;

            var ramlDocument = (new RamlBuilder().Build((IDictionary<string, object>)rawresult, filePath)).ConfigureAwait(false).GetAwaiter().GetResult();

            return ramlDocument;
        }

        public static object GetDynamicStructure(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("filePath");

            var load = Edge.Func(@"

                return function (filepath, callback) {
                    var parser = require('parser');

                    parser.parse(filepath, function (ret) {
                            callback(null, ret);
                        },
                        function(ret) {
                            callback(null, ret);
                        }
                    );
                    return;
                }
            ");

            var rawresult = load(filePath).ConfigureAwait(false).GetAwaiter().GetResult();
            return GetRaml(rawresult);
        }

    }
}
