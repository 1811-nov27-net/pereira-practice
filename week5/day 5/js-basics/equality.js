function compare(a, b)
{
    console.log("a: "+a+", b: "+b);

    //"double equals" and "triple equals"
    //  Double equals coerces the type of the values to try to "compare value without caring about type"
    //  Triple equals check the type
    console.log("a == b: "+(a == b));
    console.log("a === b: "+(a === b));
    console.log();
}

compare(5, "5");
compare({}, "");
compare(null, "");
compare(null, undefined);

//Always use triple equals aka. "strict equality" not a loose equality