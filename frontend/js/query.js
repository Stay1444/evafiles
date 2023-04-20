const urlParams = new URLSearchParams(window.location.search);
const myParam = urlParams.get('s');
console.log(myParam)

document.getElementById("search_bar").placeholder = myParam;
setTimeout(() => {
        const form  = new FormData();

        form.append("query", myParam);

        console.log(form.entries())
        
        fetch("http://192.168.0.190:5000/file/search", {
                method: "POST",
                body: form,
            
        })
}, 1000);

