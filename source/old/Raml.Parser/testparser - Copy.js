console.log('Start');


var parser = require('parser');

try {
    var container = null;
    var filepath = "/desarrollo/mulesoft/raml-dotnet-parser-2/source/Raml.Parser.Tests/Specifications/movies-v1.raml"
    parser.parseFile('raml', filepath, container);
    console.log(container);
}
catch (e) {
    console.log('Error: ' + e);
}