// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const btnToogleViewFromTableToCards = document.getElementById("change-table-cards-view");
const tableViewContainer = document.getElementById("table-layout");
const cardsViewContainer = document.getElementById("cards-layout");

let isTable = true;

if (btnToogleViewFromTableToCards) {

    btnToogleViewFromTableToCards.addEventListener('click', function () {
        isTable = !isTable;
        if (isTable) {
            tableViewContainer.classList.remove("d-none");
            cardsViewContainer.classList.add("d-none");
            btnToogleViewFromTableToCards.innerHTML = '<i class="fa-regular fa-id-card"></i>';
        } else {
            tableViewContainer.classList.add("d-none");
            cardsViewContainer.classList.remove("d-none");
            this.innerHTML = '<i class="fa-solid fa-table-list"></i>';
        };
    });

}