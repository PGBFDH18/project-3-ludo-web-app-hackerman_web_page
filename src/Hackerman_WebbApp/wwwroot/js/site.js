// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function nextTileToMoveTo() {
    
}

function MovePiece() {

    $("#tile4").append($(event.target));

}

function DiceThrow() {
    var diceNumber = document.getElementById("dicevalue").value;

    document.getElementById("center").style.display = "none";
    document.getElementById("leftUpper").style.display = "none";
    document.getElementById("leftLower").style.display = "none";
    document.getElementById("rightUpper").style.display = "none";
    document.getElementById("rightLower").style.display = "none";
    document.getElementById("rightMiddle").style.display = "none";
    document.getElementById("leftMiddle").style.display = "none";

    if (diceNumber == 1) {
        document.getElementById("center").style.display = "inline";
    }
    else if (diceNumber == 2) {
        document.getElementById("leftUpper").style.display = "inline";
        document.getElementById("rightLower").style.display = "inline";

    }
    else if (diceNumber == 3) {
        document.getElementById("leftUpper").style.display = "inline";
        document.getElementById("center").style.display = "inline";
        document.getElementById("rightLower").style.display = "inline";

    }
    else if (diceNumber == 4) {
        document.getElementById("rightLower").style.display = "inline";
        document.getElementById("rightUpper").style.display = "inline";
        document.getElementById("leftLower").style.display = "inline";
        document.getElementById("leftUpper").style.display = "inline";

    }
    else if (diceNumber == 5) {
        document.getElementById("rightLower").style.display = "inline";
        document.getElementById("rightUpper").style.display = "inline";
        document.getElementById("leftLower").style.display = "inline";
        document.getElementById("leftUpper").style.display = "inline";
        document.getElementById("center").style.display = "inline";

    }
    else if (diceNumber == 6) {
        document.getElementById("rightLower").style.display = "inline";
        document.getElementById("rightUpper").style.display = "inline";
        document.getElementById("leftLower").style.display = "inline";
        document.getElementById("leftUpper").style.display = "inline";
        document.getElementById("rightMiddle").style.display = "inline";
        document.getElementById("leftMiddle").style.display = "inline";

    }
}
/*
function changingSelect() {

    var t = document.getElementById("playerAmount");
    var finalValue = t.options[t.selectedIndex].value;

}
*/