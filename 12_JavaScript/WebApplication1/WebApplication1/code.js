//alert("hello world!");
//console.log("hello world!")

// 6.500000004 types
//var x = .1 + .2;
//console.log(typeof x, x);

//var s = "Hello O'Grady!";
//console.log(typeof s, s);

//var b = true;
//console.log(typeof b, b);

//var a = [1, false, "3"];
//console.log(typeof a);
//for (var i = 0; i < a.length; i++) {
//    console.log(a[i]);
//}

// JSON
//var o = { name: "Brock" };
//o.name = "Michael";
//o.name = null;
//delete o.name;
//console.log(o);

//console.log(o.name);
//console.log(o['name']);

//console.log(o);
//o.name = "Brock";
//console.log(o);
//o.age = 12;
//console.log(o);

//var d = new Date();
//console.log(typeof d);

//var f = function (a, b) {
//    return a + b;
//};

//function f(a, b) {
//    return a + b;
//}
//function f(a, b, c) {
//    return a + b;
//}

//console.log(typeof f);
//var result = f(2, 3);
//console.log(result);

//function f(a, b) {
//    a + b;
//}

//var u = f(2, 3);

//var u = null;
//console.log(typeof u);

// truthy or falsy
// falsy: 0, undefined, null, false, "", NaN
// truthy: !falsy
//var x = "10" - [];
//if (x) {
//    console.log(x);
//}
//else {
//    console.log(x, 'not there?@!')
//}

// ECMAScript 5
// ES6/ES2015 (Harmony)
//var a = "5";
//var b = 5;
//if (a === b) {
//    console.log('same');
//}
//else {
//    console.log('not same');
//}

//var sayHello = function(a, b, c) {
//    console.log("My name is " + this.name + " and I am " + this.age);
//}
////sayHello();

//var brock = {
//    name: "Brock", age: 5
//};
//sayHello.call(brock, 1, 2, 3);

//function foo() {
//    "use strict";
//    x = 10;
//    console.log(x);
//}

//foo();
//console.log(x);

// IIFE
(function () {
    "use strict";

    function add() {
    }

    function sub() {
    }

    window.mycoolstuff = {
        add: add, sub: sub
    };
})();
