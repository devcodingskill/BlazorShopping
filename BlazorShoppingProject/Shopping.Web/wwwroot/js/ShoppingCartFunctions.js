//function MakeUpdateQtyButtonVisible(id, visible) {
//    const updateQtyButton = document.getElementById("button[data-itemId]'" + id + "']");
//    if (visible == true)
//    {
//        updateQtyButton.style.display = "inline-block";
//    }
//    else
//    {
//        updateQtyButton.style.display = "none";
//    }
//}

function MakeUpdateQtyButtonVisible(id, visible) {
    const updateQtyButton = document.querySelector(`button[data-itemId='${id}']`);
    if (updateQtyButton) {
        if (visible == true) {
            updateQtyButton.style.display = "inline-block";
        } else {
            updateQtyButton.style.display = "none";
        }
    } else {
        console.error(`Button with data-itemId='${id}' not found.`);
    }
}
