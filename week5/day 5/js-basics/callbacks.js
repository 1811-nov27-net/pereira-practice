'use strict';

function addNumbers(a,b, callback)
{
    let result = a+b;
    return callback(result);
}

addNumbers(3,4, console.log); //prints 7

//Calbacks are important because we do a lot of listening to/waiting
// for events is JS, and also asynchronous stuff

//Often use arrow functions
addNumbers(3,4, result => console.log("calculation done"));

//Return function
function newCounter() {
    let c = 0;

    return () => {
        c++;
        return c;
    };
}

let counter = newCounter();
//Normally at this point, "c" would disapera form the stack because it has passed out of scope

console.log(counter());
console.log(counter());
console.log(counter());

let counter2 = newCounter();
console.log(counter2());
console.log(counter2());

//In JS, variables that are referenced by functions thhat are still in scope, themselves remain in scope

//In JS, functions "close over" any variables they reference

//This behavior is called "closure"
//Sometimes we call the functions themselves "closures"

//Before ES6, we wanted "namespaces", we wanted to encapsulate private details
//and expose only needed functionality
//Closure allows us to do this

//IIFE (Imediately-invoked function expression)
let library = (function(){
    let privateData = 0;
    return {
        libraryMethod() {
            return privateData;
        },
    };
})();

library.libraryMethod();