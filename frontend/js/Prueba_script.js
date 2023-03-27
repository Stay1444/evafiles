document.getElementById("upload_search_button").style.display = "none";

document.getElementById("search_bar").addEventListener("change", (e)=> {
    if (e.target.value=="")
    {
        document.getElementById("upload_button").style.display = "inline";
        document.getElementById("upload_search_button").style.display = "none";
    }
    else {
        document.getElementById("upload_button").style.display = "none";
        document.getElementById("upload_search_button".syle.display = "inline");
    }
    e.getElementById("upload_search_button").value="buscar"
console.log("funciona")

})