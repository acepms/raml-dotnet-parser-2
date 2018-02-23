using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;
using System.Linq;
using ClassLibrary1.Model;

namespace UnitTestProject1
{
    [TestClass]
    public class ApiWithExamplesTests
    {
        private WebApi model;

        [TestInitialize]
        public void Initialize()
        {
            var parser = new RamlParser();
            model = parser.Load("./specs/oas/yaml/api-with-examples.yaml").Result;
        }

        [TestMethod]
        public void Endpoints_count()
        {
            Assert.AreEqual(2, model.EndPoints.Count());
        }

        [TestMethod]
        public void Name_check()
        {
            Assert.AreEqual("Simple API overview", model.Name);
        }

        [TestMethod]
        public void Version_check()
        {
            Assert.AreEqual("v2", model.Version);
        }

        [TestMethod]
        public void Get_root_response()
        {
            var resp = model.EndPoints.First(e => e.Path == "/").Operations.First(o => o.Method == "get").Responses.First();
            Assert.AreEqual("200", resp.StatusCode);
        }
    }
}
