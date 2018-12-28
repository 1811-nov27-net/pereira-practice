//This is a "function statement", declaring a function
function my_func(a)
{
    console.log(a);
}

//We also have "function expression"
var my_func2 = (function(a){
    console.log(a);
});

//With ES6 we ALSO have "arrow functions" aka lambda
var mu_func3 = a => console.log(a);
var mu_func3 = () => console.log();
var mu_func3 = (a, b) => console.log(a);

//These all do the same thing except that arrow functions do have a subtle difference with how "this" works

//In ES5, there were only two scopes
// (remember in C# you have block scope) -
//      We can access that variable within the nearest {}
//      (and only after it's declaration)

// In ES5, we have documment scope aka global scope and function scope

var x = 5;
x = 5;

function f()
{
    console.log(x);
    if(true) {
        var x = 7;
    }

    asdf = 'asdf'; //In a function, without declaring, that's global scope

    //In a function, "var" uses function scope
    //This "x" is visible throughout my function, even at the top, before it's declared.
    //  Something called "hoisting"
}

//(Global scope undeclared)
asdf = '1234';

//ES5 has "strict mode"
'use strict';
//Strict mode turns certains silent errors into thrown errors

dasdaew = '125';

//With strict mode, undeclared variables throw errors

// ES6 added block scope to JS
// using two new ways to declare variables
// "let" and "const"

//So when you use let and const, variable only in scope
//within the nearest pair of braces. const prevents
//changing the value after first assignment.

//Use let and const always, never var or undeclared.

let obj = {
    name: 'Rogerio',
    skill: 10000,
    sayName: function() {
        console.log(this.name)
    },
    sayName2() {
        console.log(this.name)
    },
    sayNameArrow = () => console.log(this.name)
}

obj.sayName();

//In Javascrip, outside arrow functions, "this" is "unbound"/"free"

function Person(name, age)
{
    this.name = name;
    this.age = age;
    this.sayName() = obj.sayName;
    this.sayNicksName = obj.sayNameArrow
}

let person = new Person("Fred", 78);
person.sayName(); //This is resolved here, not declaration
person.sayNicksName(); //This was resolved at declaration, because it's an arrow function

console.log(person);

function Graduate(name, age, gradYear)
{
    this.__proto__ = new Person(name, age);
    this.gradYear = gradYear;
    //Could have new methods
}

let nick = new Graduate("nick", 26, 2014);
console.log(nick);

//When JS does property access (or assignment) on an object,
//it first scans the object, if nothing is found, it then looks at that
//object's __proto__, and on and on...

//IN ES6 we have proper classes with inheritance


class Person2 {
    construct(name, age) {
        this.name = name;
        this.age = age;
        this.sayName() = obj.sayName;
        this.sayNicksName = obj.sayNameArrow;
    }
}
class Graduate2 extends Person//"extends" instead of ":"
{
    constructor(name,age, gradYear) {
        super(name, age);
        this.__proto__ = new Person(name, age);
        this.gradYear = gradYear;
        //Could have new methods
    }
}

let nick2 = new Person2("asdf", 23);

//JS (ES5) IS object-oriented, but without classses
//ES6 is object-oriented with classes
//"OOP" is one paradigm of programming
//"procedural" is another - like C
//"functional" is another, where functions (behavior) are just another kind of data

//new features in ES6

/*
    let, const
    arrow functions
    class, interface
    method syntax for functions as properties
    string interpolation
    symbol type for GUIDs
    new useful built-in functions e.g string search
    Promises for async stuff without callbacks
    Native modules
        like namespaces
    built-in set and map
    "for of" loop which is like foreach
    getters and setters for properties like c#
    internationalization features
 */

 //caniuse.com to see current support for new things

 //String interpolation
 console.log("person's name: "+nick.name);
 console.log(`person's name: ${nick.name}`);
 //console.log($"person's name: {nick.name}"); //C# way, similar but no