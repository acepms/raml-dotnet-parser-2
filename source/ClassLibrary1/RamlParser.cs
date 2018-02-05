using EdgeJs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class RamlParser
    {
        public object Load(string filePath)
        {
            var rawresult = GetDynamicStructureAsync(filePath).Result;
            return rawresult;
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
