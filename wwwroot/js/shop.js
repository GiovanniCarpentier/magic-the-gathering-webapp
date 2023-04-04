window.addEventListener("DOMContentLoaded",init);

function init(){
    addListenerToCards();
}

function addListenerToCards(){
    cards = document.querySelectorAll(".card");
    cards.forEach(card => {
        let id = card.getAttribute("value");
        let button = card.querySelector(".addCart")
        button.addEventListener("click", (e) => {
            e.preventDefault();
            onAddToCart(id)
        } );
    });
}

function onAddToCart(id){
    fetch(api + "/ShoppingCart/add/" + id)
}