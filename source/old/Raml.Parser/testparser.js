

var parser = require('parser');
var util = require('util')

setInterval(function () {
    console.log('Start');

    try {
        var filepath = "file://./ConsoleApp1/movies-v1.raml"
        parser.parse('raml', filepath, function (m) {
            console.log(util.inspect(m, { showHidden: false, depth: null }))
        }, function (error) {
            console.log(error);
        });

    }
    catch (e) {
        console.log('Error: ' + e);
    }
}, 1000);