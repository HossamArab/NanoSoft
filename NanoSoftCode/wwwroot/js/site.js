﻿// shuffle
// Get all buttons with class="btn" inside the container
var btns = document.getElementsByClassName("filter");

// Loop through the buttons and add the active class to the current/clicked button
for (var i = 0; i < btns.length; i++) {
    btns[i].addEventListener("click", function () {
        var current = document.getElementsByClassName("selected");
        current[0].className = current[0].className.replace("selected", " ");
        this.className += " selected";
    });
}
var mixer = mixitup('.suffle-container');