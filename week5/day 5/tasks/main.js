function parseJsonText()
{
    try{
        let textValue = document.getElementById("textArea");
        let jsonString = JSON.parse(textValue.value);
        console.log(jsonString);
    }
    catch (e)
    {
        console.log(e);
    }
}

document.addEventListener("DOMContentLoaded", () => {
    let apiButton = document.getElementById("apiButton");
    let apiReturn = document.getElementById("apiReturn");

    apiButton.addEventListener("click", () => {
        // fetch API, modern way to do AJAX
        fetch("https://swapi.co/api/people/1")
            .then(response => response.json())
            .then(obj => {
                let jsonReturn =    "name: "+obj.name+"<br/>"+
                                    "height: "+obj.height+"<br/>"+
                                    "mass: "+obj.mass+"<br/>"+
                                    "hair_color: "+obj.hair_color+"<br/>"+
                                    "skin_color: "+obj.skin_color+"<br/>"+
                                    "eye_color: "+obj.eye_color+"<br/>"+
                                    "birth_year: "+obj.birth_year+"<br/>"+
                                    "gender: "+obj.gender+"<br/>"+
                                    "homeworld: "+obj.homeworld+"<br/>"
                apiReturn.innerHTML = jsonReturn;
            })
            .catch(err => console.log(err));
    });


});