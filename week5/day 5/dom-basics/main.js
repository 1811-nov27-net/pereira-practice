'use strict';

function alertMe()
{
    alert("You clicked the element");
}

//We access most of DOM API using document object
//let col1 = document.getElementById("col1");
//col1.onclick = alertMe(); // <-- Wrong
//col1.onclick = alertMe; 

//Scripts are run as soon as they're encoutered in the HTML
//and elements are created in the browser's memory
//as soon as they're encoutered in the page

window.onload = function() {
    //this is the basic way of waiting until the document is all loaded before trying to interact with it
    
    //let col1 = document.getElementById("col1");
    //col1.onclick = alertMe(); // <-- Wrong
    //col1.onclick = alertMe; 
};

//better way that allows multiple functions to all listen on the same event
window.addEventListener("load", function(){
    //let col1 = document.getElementById("col1");
    //col1.onclick = alertMe; 
});

function printEventDetail(event)
{
    console.log(event);
    console.log(event.target);
    console.log(this);
}

//Don't wait all the way for every asset to be loaded, just all the elements created in DOM
document.addEventListener("DOMContentLoaded", () => {
    let col1 = document.getElementById("col1");
    col1.addEventListener("click", alertMe); 

    let header = document.getElementById("header");
    header.innerHTML += ", changed by JS";
    header.innerHTML = `<u>${header.innerHTML}<u>`;

    //jQuery, common, JS library, makes a lot of these basic DOM tasks faster to write, more readable
    let cell1 = document.getElementById("cell1");
    cell1.addEventListener("click", printEventDetail);

    let tbody = document.getElementById("tbody");
    //By default, event listeners/handlers are in bubbling mode
    tbody.addEventListener("click", printEventDetail);
    //"true" as third parameter will set capturing mode
    tbody.addEventListener("click", printEventDetail, true);
});

