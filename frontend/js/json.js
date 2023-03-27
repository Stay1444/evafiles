var name = document.getElementById("filename")
var description = document.getElementById("description")
var custom_url =document.getElementById("custom_url")
var available_time=document.getElementById("available_time")
var jsonbtn= document.getElementById("jsonbtn")

jsonbtn.onclick = () => {
    var data = {
        name: filename.value,
        description:description.value,
        custom_url:custom_url.value,
        available_time:available_time.value,

    }
    console.log(JSON.stringify(data))
    console.log(available_time)
}