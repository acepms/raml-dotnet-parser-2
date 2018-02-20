using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;
using System.Linq;
using ClassLibrary1.Model;

namespace UnitTestProject1
{
    [TestClass]
    public class MoviesTests
    {
        private WebApi model;

        [TestInitialize]
        public void Initialize()
        {
            var parser = new RamlParser();
            model = parser.Load("file://./specs/movies-v1.raml").Result;
        }

        [TestMethod]
        public void Movies_Endpoints_should_be_9()
        {
            Assert.AreEqual(9, model.EndPoints.Count());
        }

        [TestMethod]
        public void Movies_schemes_should_be_http()
        {
            Assert.AreEqual(1, model.Schemes.Count());
            Assert.AreEqual("HTTP", model.Schemes.First());
        }

        [TestMethod]
        public void Movies_version_should_be_1()
        {
            Assert.AreEqual("1.0", model.Version);
        }

        [TestMethod]
        public void Movies_basepath_should_be_api()
        {
            Assert.AreEqual("/api", model.BasePath);
        }

        [TestMethod]
        public void Movies_host_should_be_movies_com()
        {
            Assert.AreEqual("movies.com", model.Host);
        }

        [TestMethod]
        public void Movies_name_should_be_movies_v_1()
        {
            Assert.AreEqual("Movies v 1", model.Name);
        }

        [TestMethod]
        public void Movies_Post_Operation_Security()
        {
            Assert.AreEqual(1, model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.Count());
            Assert.AreEqual("OAuth 2.0", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Type);
            Assert.AreEqual("https://localhost:8081/oauth/authorize", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Settings.AuthorizationUri);
            Assert.AreEqual("https://localhost:8081/oauth/access_token", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Settings.AccessTokenUri);
            Assert.AreEqual("authorization_code", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Settings.AuthorizationGrants.First());
            Assert.AreEqual("read", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Settings.Scopes.First().Name);
            Assert.AreEqual("write", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Settings.Scopes.Last().Name);
            Assert.AreEqual("access_token", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().QueryParameters.First().Name);
            Assert.AreEqual("Authorization", model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "post").Security.First().Headers.First().Name);
        }

        [TestMethod]
        public void Movies_get_response()
        {
            var resp = model.EndPoints.First(e => e.Path == "/movies").Operations.First(o => o.Method == "get").Responses.First();
            Assert.AreEqual("200", resp.StatusCode);
            Assert.AreEqual(1, resp.Payloads.Count());
            Assert.AreEqual("application/json", resp.Payloads.First().MediaType);

        }
    }
}
