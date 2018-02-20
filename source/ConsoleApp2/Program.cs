using ClassLibrary1;
using ClassLibrary1.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestProject1;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            //var a = Test().Result;
            //Console.WriteLine(a);
            RunTests();
        }

        private static void RunTests()
        {
            var tests = new MoviesTests();
            try
            {
                tests.Initialize();
                tests.Movies_Endpoints_should_be_9();
                tests.Movies_Post_Operation_Security();
                tests.Movies_schemes_should_be_http();
                tests.Movies_basepath_should_be_api();
                tests.Movies_version_should_be_1();
                tests.Movies_get_response();
                Console.WriteLine("Succeeded");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed");
                Console.WriteLine(ex.Message);
            }
        }


        private async static Task<WebApi> Test()
        {
            var parser = new RamlParser();
            var a = await parser.Load("file:///desarrollo/mulesoft/raml-dotnet-parser-2/source/Raml.Parser.Tests/Specifications/movies-v1.raml");

            return a;
        }
    }
}
