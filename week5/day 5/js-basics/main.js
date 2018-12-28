console.log("main.js");

//JS is dynamically typed
//Variables have no types, only values have types

//JS is interpreted, not compiled
//JS is run in the browser
//  or, server-side with things like Node.JS

var x;

//numbers
//for integers, floating-point, whatever
// 64-b floating-point number
x = 5;
x = 5.5;
//We have all the stuff the IEEE floating points are suposed to have
x = Infinity;
x = -Infinity;
x = -0;
x = NaN; //"Not a Number" is a number type
x = 5/0; //Returns Infinity

//String
//Single or double quotes, same thins
x = 'dasd';
x = "dasd";

//Booleans
x = true;
x = false;

//Null
//typeof lies and says it's object, but this has been kept for back compability
x = null;

//Object
//Works like "dunamic" in c#
//get or set any property without declaring it
//We don't use classes as templates for objects in JS
x = {};
x.asdf = true;
x.erver = 'abc';

//We can access properties of objects with indexing syntax or dot syntax
console.log(x["asdf"]);

//There's syntax for arrays but they are ust objects as well
x = [1,2,3];

//Functions are first-class objects
//Functions are really just "object" type
//  But typeof does call then "function"
//Functions have parameters names, but not parameters types, or return types
function my_function(a, b, c = 5)
{
    /*if(a == 1)
        return a;*/
    console.log(b);
    console.log(c);
}
//If no return statement, it returned undefined

//un-passed parameters will be "undefined"
x = my_function();


x = my_function;
x.ser = "dsada";

//Undefined
//x = undefined;

console.log(x);
console.log("value of x:" + x);
console.log("type of x:" + typeof(x));

console.log("value of x.notreal:" + x.notreal);
console.log("type of x.notreal:" + typeof(x.notreal));

//We standardized JavaScript under the name "ECMAScript" or "ES" for short
//"what version of JavaScript are we using"
//ES5
//  This is the baseline for all modern JS
//  Because all browsers support it
//  prototypal inheritance
//ES6 aka ES2015
//  addded classes + interfaces
//ES2016
//ES2017

//symbol
//(added in ES5, for GUIDs, unique IDs for things)

//There are lots of other languages that people have come up with that extend JavaScript
//and compile down to JavaScript
//  Typescrit, made by Microsoft
//      adds opt-in strict typing to JavaScript
//  CoffeeScript

//Sometimes people call this "transpilation"
//often we say we transpile to JavaScript

//We also transpile ES6 to ES5
//Any higher version we can transpile to a lower version