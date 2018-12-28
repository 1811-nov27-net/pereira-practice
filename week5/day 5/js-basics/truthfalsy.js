'use strict';

let x = "1234";

//In JS, we try to avoid the "automatic type coercion" (==)
//usualy
//except in if-conditions

if(x)
{
    console.log("x is truthy");
}

//Truthy : converts to true as a boolean
//Falsy : converts to false as a boolean

//All values are truthy except a few exceptions
/*
    0 (and -0)
    NaN
    ""
    null
    undefined
    false
*/
//everything eles, including empty array, empety objects
//any other objects, any functions, etc. is truthy
//even "false" is truthy.