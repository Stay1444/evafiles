const name = document.getElementById("filename")
const description = document.getElementById("description")
const available_time=document.getElementById("available_time")
const fileInput = document.getElementById("myFile");

var jsonbtn= document.getElementById("jsonbtn")

jsonbtn.onclick = () => {
    if (fileInput.files.length < 1) alert("Select atleast one file!");

    const formData  = new FormData();
    
    formData.append('name', name.value);
    formData.append('description', description.value);
    formData.append('duration', duration.value);

    //const fileData = new FileReader().readAsArrayBuffer(fileInput.files[0]);

//    formData.append('file', fileData);

    formData.append('file', fileInput.files[0]);

    fetch("http://127.0.0.1:5000/file/upload", {
        method: "POST",
        body: formData
    }).then(x => {
        console.log(x);
    });
}