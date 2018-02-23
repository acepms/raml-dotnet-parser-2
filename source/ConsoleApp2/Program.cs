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
            try
            {
                RunGeneralTests().Wait();
                RunChinookTests();
                RunMoviesTests();
                RunApiWithExamplesTests();
                Console.WriteLine("Succeeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed");
                Console.WriteLine(ex.Message);
                if(ex.InnerException != null)
                    Console.WriteLine(ex.InnerException.Message);
            }
        }

        private static async Task RunGeneralTests()
        {
            var tests = new GeneralTests();
            await tests.Should_detect_OAS_type_from_json_file();
            await tests.Should_detect_OAS_type_from_yaml_file();
            await tests.Should_detect_RAML_type_from_file_contents();
            await tests.Should_detect_RAML_type_from_extension();
            await tests.Should_accept_file_without_prefix();
            await tests.Should_accept_file_with_prefix();
            await tests.Should_throw_if_file_not_exists();
        }

        private static void RunApiWithExamplesTests()
        {
            var apiWithExamplesTests = new ApiWithExamplesTests();
            apiWithExamplesTests.Initialize();
            apiWithExamplesTests.Name_check();
            apiWithExamplesTests.Version_check();
            apiWithExamplesTests.Endpoints_count();
            apiWithExamplesTests.Get_root_response();
        }

        private static void RunChinookTests()
        {
            var chinookTests = new ChinookTests();
            chinookTests.Initialize();
            chinookTests.Name_should_be_chinook_raml_1_api();
            chinookTests.Endpoints_should_be_10();
            chinookTests.Get_customers_response();
            chinookTests.Get_albums_response();
        }

        private static void RunMoviesTests()
        {
            var moviesTests = new MoviesTests();
            moviesTests.Initialize();
            moviesTests.Endpoints_should_be_9();
            moviesTests.Post_Operation_Security();
            moviesTests.Schemes_should_be_http();
            moviesTests.Basepath_should_be_api();
            moviesTests.Version_should_be_1();
            moviesTests.Get_response();
            moviesTests.Post_request();
        }

        private async static Task<WebApi> Test()
        {
            var parser = new RamlParser();
            var a = await parser.Load("/desarrollo/mulesoft/raml-dotnet-parser-2/source/Raml.Parser.Tests/Specifications/movies-v1.raml");

            return a;
        }
    }
}
