document.getElementById("search_button").style.display = "none";

document.getElementById("search_bar").addEventListener("keydown", (e) => {
  if (e.target.value.trim() == "") {
    document.getElementById("upload_button").style.display = "inline";
    document.getElementById("search_button").style.display = "none";
  } else {
    document.getElementById("upload_button").style.display = "none";
    document.getElementById("search_button").style.display = "inline";
  }
});

document.getElementById("search_bar").addEventListener("input", (e) => {
  if (e.target.value.trim() != "") {
    document.getElementById("upload_button").style.display = "none";
    document.getElementById("search_button").style.display = "inline";
  } else {
    document.getElementById("upload_button").style.display = "inline";
    document.getElementById("search_button").style.display = "none";
  }

});

document.getElementById("upload_button").onclick = (e)=> {
  window.location='upload.html';
}
document.getElementsByClassName("titulo")[0].onclick = (e)=> {
  window.location='index.html';
}
document.getElementById("search_button").onclick = (e) =>{
  //window.location='search_result.html';
  window.location = 'search_result.html?v=' + document.getElementById('search_bar').value;
}
