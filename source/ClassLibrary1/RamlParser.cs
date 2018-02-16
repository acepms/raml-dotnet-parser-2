using ClassLibrary1.Mappers;
using ClassLibrary1.Model;
using EdgeJs;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class RamlParser
    {
        public WebApi Load(string filePath)
        {
            var rawresult = GetDynamicStructureAsync(filePath).Result;
            var ret = rawresult as IDictionary<string, object>;
            var error = ret["error"];
            if (error != null)
                throw new FormatException(error as string);
            var model = ret["model"] as IDictionary<string, object>;
            var webApi = new WebApiMapper().Map(model);
            return webApi;
        }

        public static async Task<object> GetDynamicStructureAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("filePath");

            var func = Edge.Func(@"
                return function (file, callback) {
                    var parser = require('parser')
                    parser.parse('raml', file, function(model) { return callback(null, model); });
                }
            ");

            var rawresult = await func(filePath);
            return rawresult;
        }
    }
}
