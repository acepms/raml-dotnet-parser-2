using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;
using System.Linq;
using ClassLibrary1.Model;

namespace UnitTestProject1
{
    [TestClass]
    public class ChinookTests
    {
        private WebApi model;

        [TestInitialize]
        public void Initialize()
        {
            var parser = new AmfParser();
            model = parser.Load("./specs/chinook-v1.raml").Result;
        }

        [TestMethod]
        public void Endpoints_should_be_10()
        {
            Assert.AreEqual(10, model.EndPoints.Count());
        }

        [TestMethod]
        public void Name_should_be_chinook_raml_1_api()
        {
            Assert.AreEqual("Chinook RAML 1 Api", model.Name);
        }

        [TestMethod]
        public void Get_customers_response()
        {
            var resp = model.EndPoints.First(e => e.Path == "/customers").Operations.First(o => o.Method == "get").Responses.First();
            Assert.AreEqual("200", resp.StatusCode);
            Assert.AreEqual(1, resp.Payloads.Count());
            Assert.AreEqual("application/json", resp.Payloads.First().MediaType);
            Assert.IsInstanceOfType(resp.Payloads.First().Schema, typeof(ArrayShape));
            var array = (ArrayShape)resp.Payloads.First().Schema;
            Assert.IsInstanceOfType(array.Items, typeof(NodeShape));
            var node = (NodeShape)array.Items;
            Assert.AreEqual(15, node.Properties.Count());
        }

        [TestMethod]
        public void Get_albums_response()
        {
            var resp = model.EndPoints.First(e => e.Path == "/albums").Operations.First(o => o.Method == "get").Responses.First();
            Assert.AreEqual("200", resp.StatusCode);
            Assert.AreEqual(1, resp.Payloads.Count());
            Assert.AreEqual("application/json", resp.Payloads.First().MediaType);
            Assert.IsInstanceOfType(resp.Payloads.First().Schema, typeof(ArrayShape));
            var array = (ArrayShape)resp.Payloads.First().Schema;
            Assert.IsInstanceOfType(array.Items, typeof(NodeShape));
            var node = (NodeShape)array.Items;
            Assert.AreEqual(3, node.Properties.Count());
        }

        //[TestMethod]
        //public void Post_Operation_Security()
        //{
        //    Assert.AreEqual(1, model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.Count());
        //    Assert.AreEqual("OAuth 2.0", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Type);
        //    Assert.AreEqual("https://localhost:8081/oauth/authorize", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Settings.AuthorizationUri);
        //    Assert.AreEqual("https://localhost:8081/oauth/access_token", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Settings.AccessTokenUri);
        //    Assert.AreEqual("authorization_code", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Settings.AuthorizationGrants.First());
        //    Assert.AreEqual("read", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Settings.Scopes.First().Name);
        //    Assert.AreEqual("write", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Settings.Scopes.Last().Name);
        //    Assert.AreEqual("access_token", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().QueryParameters.First().Name);
        //    Assert.AreEqual("Authorization", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Headers.First().Name);
        //}


        //private static void PropertiesAsserts(NodeShape node)
        //{
        //    var idProp = node.Properties.First(p => p.Path.EndsWith("#id"));
        //    Assert.IsInstanceOfType(idProp.Range, typeof(ScalarShape));
        //    Assert.IsTrue(idProp.Required);
        //    var id = (ScalarShape)idProp.Range;
        //    Assert.IsTrue(id.DataType.EndsWith("#integer"));
        //    Assert.AreEqual("id", id.Name);

        //    var nameProp = node.Properties.First(p => p.Path.EndsWith("#name"));
        //    Assert.IsTrue(nameProp.Required);
        //    var name = (ScalarShape)nameProp.Range;
        //    Assert.IsTrue(name.DataType.EndsWith("#string"));
        //    Assert.AreEqual(255, name.MaxLength);
        //    Assert.AreEqual("name", name.Name);

        //    var durationProp = node.Properties.First(p => p.Path.EndsWith("#duration"));
        //    Assert.IsFalse(durationProp.Required);
        //    var duration = (ScalarShape)durationProp.Range;
        //    Assert.IsTrue(duration.DataType.EndsWith("#float"));
        //    Assert.AreEqual("1", duration.Minimum);
        //    Assert.AreEqual("duration", duration.Name);

        //    var storylineProp = node.Properties.First(p => p.Path.EndsWith("#storyline?"));
        //    Assert.IsFalse(storylineProp.Required);
        //    Assert.AreEqual("storyline?", storylineProp.Range.Name);

        //    Assert.AreEqual(1, node.Examples.Count());
        //    Assert.IsTrue(node.Examples.First().Value.Length > 0);
        //}

        //[TestMethod]
        //public void Post_request()
        //{
        //    var request = model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Request;
        //    Assert.AreEqual(1, request.Payloads.Count());
        //    Assert.AreEqual("application/json", request.Payloads.First().MediaType);
        //    Assert.AreEqual("Movie", request.Payloads.First().Schema.Name);
        //    Assert.IsInstanceOfType(request.Payloads.First().Schema, typeof(NodeShape));
        //    var node = (NodeShape)request.Payloads.First().Schema;
        //    Assert.AreEqual(9, node.Properties.Count());

        //    PropertiesAsserts(node);
        //}
    }
}
