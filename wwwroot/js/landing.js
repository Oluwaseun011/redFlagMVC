const menu = document.querySelector('.log-img')
const dash = document.querySelector('.dashboard')
const bdg = document.querySelector('.container')


menu.addEventListener('click', () => {
    if (dash.classList.contains("pop-menu")) {
        dash.classList.remove('pop-menu')
    }
    else {
        dash.classList.add('pop-menu')
    }
})


//* Loop through all dropdown buttons to toggle between hiding and showing its dropdown content - This allows the user to have multiple dropdowns without any conflict */
var dropdown = document.getElementsByClassName("dropdown-btn");
var i;

for (i = 0; i < dropdown.length; i++) {
    dropdown[i].addEventListener("click", function () {
        this.classList.toggle("active");
        var dropdownContent = this.nextElementSibling;
        if (dropdownContent.style.display === "block") {
            dropdownContent.style.display = "none";
        } else {
            dropdownContent.style.display = "block";
        }
    });
}

