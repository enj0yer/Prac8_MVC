function addToBasket(element) {

    let id = Number(element.id.split("_")[1]);
    let number = Number(document.getElementById(`number_${id}`).innerHTML);

    $.ajax({
        type: "post",
        url: "/Ajax/AddToBasket",
        data: "id=" + id + "&" + "number=" + number,
        success: function (response) {
            createToast(response.Result);
        },
        error: function (e) {
            console.log(e);
        }
    });
}

function deleteProduct(element) {
    let id = Number(element.id.split("_")[1]);

    $.ajax({
        type: "post",
        url: "/Ajax/DeleteProduct",
        data: "id=" + id,
        success: function (response) {
            if (response.Status == "SUCCESS") {
                document.getElementById(`product_${id}`).remove();
            }
            else {
                createToast(response.Result);
            }
        }
    });
}

function deleteOrder(element) {
    let id = Number(element.id.split("_")[1]);

    $.ajax({
        type: "post",
        url: "/Ajax/DeleteOrder",
        data: "id=" + id,
        success: function (response) {
            if (response.Status == "SUCCESS") {
                document.getElementById(`order_${id}`).remove();
            }
            else {
                createToast(response.Result);
            }
        },
        error: function (e) {
            console.log(e);
        }
    });
}

function increase(element) {
    let id = element.id.split("_")[1];
    let number = document.getElementById(`number_${id}`);

    if (Number(number.innerHTML) < 10) {
        number.innerHTML = Number(number.innerHTML) + 1;
    }
}

function decrease(element) {
    let id = element.id.split("_")[1];
    let number = document.getElementById(`number_${id}`);

    if (Number(number.innerHTML) > 1) {
        number.innerHTML = Number(number.innerHTML) - 1;
    }
}

function createToast(message) {
    let toast = document.createElement("div");

    let number = document.querySelectorAll(".toast").length;

    toast.className = "alert";
    toast.id = "toast_" + number;
    toast.style = "width: max-content; height: max-content; position: fixed; top: 30px; left: 30px; z-index: 1050; background-color: yellow; padding: 20px; opacity: 0.7";
    toast.innerHTML = message;
    document.querySelector("body").append(toast);
    setTimeout(() => {
        document.getElementById(toast.id).remove();
    }, 2000);
}

