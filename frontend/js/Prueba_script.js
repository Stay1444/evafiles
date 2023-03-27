document.getElementById("search_button").style.display = "none";

document.getElementById("search_bar").addEventListener("keydown", (e)=> {
    if (e.target.value.trim ()=="")
    {
        document.getElementById("upload_button").style.display = "inline";
        document.getElementById("search_button").style.display = "none";
    }
    else {
        document.getElementById("upload_button").style.display = "none";
        document.getElementById("search_button").style.display = "inline";
    }
    e.getElementById("search_button").value="buscar"
console.log("funciona")
});

document.getElementById("search_bar").addEventListener("input", (e)=> {
if (e.target.value.trim ()!="")
{
    document.getElementById("upload_button").style.display = "none";
    document.getElementById("search_button").style.display = "inline";
} else  {
    document.getElementById("upload_button").style.display = "inline";
    document.getElementById("search_button").style.display = "none";
}
e.getElementById("search_button").value="subir"

});