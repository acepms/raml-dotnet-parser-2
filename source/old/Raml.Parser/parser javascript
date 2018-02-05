console.log('Start');

if (process.argv.length <= 2) {

    console.log('Error: Path not specified');

} else {

    var filepath = process.argv[2];

    var fs = require('fs');
    var amf = require('amf');
    amf.plugins.document.WebApi.register();
    amf.plugins.document.Vocabularies.register();
    amf.plugins.features.AMFValidation.register();

    try {
        stats = fs.lstatSync(filepath);

        if (stats.isFile()) {


            var path = require('path');
            amf.Core.init().then(function () {
              
              console.log('Inside init');
              var parser = amf.Core.parser("RAML 1.0", "application/yaml");
              console.log('Parser created');
              parser.parseFile("file://" + filepath, { success: success, error: error });

            });    

        } else {
            
            console.log('Error: ' + path + ' does not exist or is not a file');

        }
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
}