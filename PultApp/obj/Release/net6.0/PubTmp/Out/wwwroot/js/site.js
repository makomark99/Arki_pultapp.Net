// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function hideEmailDiv() {
    let emailDiv = document.getElementById("email");
    if ($('input[id=Radios1]:checked').length > 0) {
        emailDiv.removeAttribute('style', '');
    }
    else {
        emailDiv.setAttribute('style', 'display:none;');
    }
}
  const range = document.getElementById('range');
const value = document.getElementById('value');
const emoji = document.getElementById('emoji');
const customerSatisfaction = [
    'Elégedetlen!',
    'Valamennyire elégedett!',
    'Elégedett!',
    'Nagyon elégedett!',
];
const customerSatisfactionEmojis = ['😠', '😐', '😊', '😀'];
  range.addEventListener('input', () => {
      value.textContent = `${customerSatisfaction[range.value - 1]}`;
      emoji.textContent = `${customerSatisfactionEmojis[range.value - 1]}`;
  });
  range.addEventListener('input', () => {
  let percent = (range.value - range.min) / (range.max - range.min);
  range.style.background = `linear-gradient(to right, #4caf50 ${percent * 100}%, #222 0%) `;
    });


$(document).ready(function () {
    $('#myTable').DataTable();
});

//form
let firstForm = document.getElementById("firstForm")
let secondForm = document.getElementById("secondForm");
let thirdForm = document.getElementById("thirdForm")
let thanks = document.getElementById("thanks")

function showThanks() {
    thirdForm.setAttribute('style', 'display:none;');
    thanks.removeAttribute('style', '');
}
function showSecond() {
    secondForm.removeAttribute('style', '');   
    firstForm.setAttribute('style', 'display:none;');  
    thirdForm.setAttribute('style', 'display:none;');
}
function showFirst() {
    firstForm.removeAttribute('style', '');
    secondForm.setAttribute('style', 'display:none;');
}
function showThird() {
    thirdForm.removeAttribute('style', '');
    secondForm.setAttribute('style', 'display:none;');
}