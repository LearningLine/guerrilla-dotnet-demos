
var express = require('express');
var app = express();

app.use(function (req, resp, next) {
	console.log(req.path);
	next();
	console.log(resp.statusCode);
});

app.use(express.static(__dirname + "/public"));

app.get('/api/people', function(req, resp){
	resp.send([
		{name:'Brock'},
		{name:'Alice'},
		{name:'Bob'}
	]);
});

// app.use(function (req, resp, next) {
// 	resp.send("<h1>Hello Express!</h1>");
// });

console.log("running from " + __dirname);

app.listen(3000);
