// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

 $(document).ready(function () {
     $('#myTable').DataTable({
         
         "oLanguage": {
             "sSearch": "Keresés:",
         },
         "language": {
             "paginate": {
                 "first": "Első",
                 "last": "Utolsó",
                 "next": "Következő",
                 "previous": "Előző"
             },
             "info": "Mutatva _START_ - _END_-ig a _TOTAL_ találatból",
             "infoFiltered": "(szűrve _MAX_ találatból)",
             "infoPostFix": "",
             "zeroRecords": "A keresésnek nincs találata",
             "infoEmpty": "Nincs találat",
             "lengthMenu": '<select class="">' +
                 '<option value="5">5</option>' +
                 '<option value="10">10</option>' +
                 '<option value="25">25</option>' +
                 '<option value="50">50</option>' +
                 '<option value="100">100</option>' +
                 '<option value="-1">Össz</option>' +
                 '</select> találat / oldal '
         }
         //columns: [
         //    { title: "Feliratkozott E-mail" },
         //    { title: "Feliratkozva?" },
         //    { title: "Honnan hallott rólunk?" },
         //    { title: "😊" },
         //    { title: "Elégedettség (1-4)" },
         //    { title: "Bejegyezve" }
         //]
     });
 });


function hideEmailDiv() {
    let emailDiv = document.getElementById("email");
    if ($('input[id=Radios1]:checked').length > 0) {
        emailDiv.removeAttribute('style', '');
        showThirdButton.classList.add("disabled")
    }
    else {
        emailDiv.setAttribute('style', 'display:none;');
        showThirdButton.classList.remove("disabled")
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

//form
let firstForm = document.getElementById("firstForm")
let secondForm = document.getElementById("secondForm");
let thirdForm = document.getElementById("thirdForm")
let thanks = document.getElementById("thanks")
let emailInput = document.getElementById("email")
let showThirdButton = document.getElementById("thirdBtn")

function showThanks() {
    thirdForm.setAttribute('style', 'display:none;');
    thanks.removeAttribute('style', '');
}
function showSecond() {
    secondForm.removeAttribute('style', '');   
    firstForm.setAttribute('style', 'display:none;');  
    thirdForm.setAttribute('style', 'display:none;');
    if ($('input[id=Radios1]:checked').length != 0) {
        showThirdButton.classList.add("disabled")
    }
}
function showFirst() {
    firstForm.removeAttribute('style', '');
    secondForm.setAttribute('style', 'display:none;');
}
function showThird() {
    function nextFormPage() {
        thirdForm.removeAttribute('style', '');
        secondForm.setAttribute('style', 'display:none;');
    }
    if (emailInput.hasAttribute('style', 'display:none;')) {
        showThirdButton.classList.remove("disabled")
        nextFormPage()
    }      
}

function isValidEmail(email) {
    if (email[0] === '@' || email[email.length - 1] === '@') {
        return false;
    }
    if (email[0] === '.' || email[email.length - 1] === '.') {
        return false;
    }
    if (email.indexOf('@') === -1 || email.indexOf('.') === -1) {
        return false;
    }
    // Check if @ and . are not neighbors
    if (email.indexOf('@') + 1 === email.indexOf('.') || email.indexOf('.') + 1 === email.indexOf('@')) {
        return false;
    }
    return true;
}

let intervalId;
let email = document.getElementById("emailInput");
let emailError = document.getElementById("emailError")
function checkEmail() {
    
    //intervalId = setInterval(function () {
       
        if (isValidEmail(email.value)) {
            emailInput.removeAttribute("style", " border: 3px solid yellow; border-radius:10px")
            emailError.classList.add("d-none")
            showThirdButton.classList.remove("disabled")
            clearInterval(intervalId);
        } else {

            email.setAttribute("style"," border: 3px solid yellow; border-radius:10px");
            emailError.classList.remove("d-none")
        }
    //}, 500);
}
 