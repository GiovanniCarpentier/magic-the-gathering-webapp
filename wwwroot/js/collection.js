window.addEventListener("DOMContentLoaded",init);

function init(){
    addListenerToCards();
}

function addListenerToCards(){
    cards = document.querySelectorAll(".card");
    cards.forEach(card => {
        card.addEventListener("click", onclickCard);
    });
}

function onclickCard(e){
    e.preventDefault();
    currentCard = e.currentTarget;
    id = currentCard.getAttribute("value");
    name = currentCard.querySelector(".card-title").textContent;
    sendSelectedCardToApi(id)

    checkMyCollection(id,name)

    currentCard.classList.toggle("inMyCollection")
}

function sendSelectedCardToApi(id){
    fetch(api + "/Collection/card/" + id)
}

function checkMyCollection(id, name){
    myCollection = document.querySelector("#MyCollectionCards")
    card = myCollection.querySelector(`div[value='${id}']`)
    if(card != null){
        removeFromCollection(card)
    }else{
        addToMyCollection(id,name, myCollection)
    }

}

function removeFromCollection(node){
    node.remove()
}

function addToMyCollection(id, name, myCollection){
    myCollection.innerHTML += `<div class="MyCollectionCard" value="${id}"> 
                        <h5>${name}</h5> 
                        </div>` 
}