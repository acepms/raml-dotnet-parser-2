console.log('Start');


var parser = require('parser');

try {

    var filepath = "/desarrollo/mulesoft/raml-dotnet-parser-2/source/Raml.Parser.Tests/Specifications/movies-v1.raml"
    parser.parse(filepath, function (ret) {
                    console.log('success');
                    console.log(ret);
                },
                function(ret) {
                    console.log('error');
                    console.log(ret);
                }
    );
}
catch (e) {

    console.log('Error: ' + e);

}


function success(doc) {
    console.log('parser has respond with: ' + doc)
}

function error(exception) {
    console.log("Error", exception)
}
