using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = Test().Result;
            Console.WriteLine(a);
        }

        private async static Task<ClassLibrary1.Model.WebApi> Test()
        {
            var parser = new RamlParser();
            var a = await parser.Load("file:///desarrollo/mulesoft/raml-dotnet-parser-2/source/Raml.Parser.Tests/Specifications/movies-v1.raml");
            return a;
        }
    }
}
