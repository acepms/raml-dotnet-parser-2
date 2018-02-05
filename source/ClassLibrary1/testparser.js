

var parser = require('parser');
var util = require('util')

console.log('Start');

var filepath = "file:///desarrollo/mulesoft/raml-dotnet-parser-2/source/Raml.Parser.Tests/Specifications/movies-v1.raml"
parser.parse('raml', filepath, function (m) {
    console.log(m);
});