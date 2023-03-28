const urlParams = new URLSearchParams(window.location.search);
const myParam = urlParams.get('s');
console.log(myParam)

document.getElementById("search_bar").placeholder = myParam;
const form  = new FormData();

form.append("Query", myParam )
console.log(form)
fetch("http://192.168.0.190:5000/file/search", {
        method: "POST",
        body: form,
    
})
