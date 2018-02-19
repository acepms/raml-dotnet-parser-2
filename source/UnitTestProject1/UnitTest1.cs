using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary1;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task Movies_RAML1()
        {
            var parser = new RamlParser();
            var model = await parser.Load("file:///desarrollo/mulesoft/raml-dotnet-parser-2/source/Raml.Parser.Tests/Specifications/movies-v1.raml");
            Assert.AreEqual(9, model.EndPoints.Count());
        }

        [TestMethod]
        public void Movies_RAML1_2()
        {
            var parser = new RamlParser();
            var model = parser.Load("file:///desarrollo/mulesoft/raml-dotnet-parser-2/source/Raml.Parser.Tests/Specifications/movies-v1.raml").Result;
            Assert.AreEqual(9, model.EndPoints.Count());
        }
    }
}
