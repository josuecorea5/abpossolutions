$(document).ready(function () {
    const totalProducts = $("#total-products");
    const client = $("#client");
    const description = $("#description");
    const phone = $("#phone");
    const btnCreateSale = $("#createSale");
    const errorMessage = $("#error-message");
    let productsTable = $(".productsAdded");
    let products = [];
    let total = 0;
    $(".btnAddSale").each(function () {
        $(this).on("click", function (event) {
            event.preventDefault();
            const idProduct = $(this).data("id");
            let quantity = 0;
            const unitPrice = $(this).parent().parent().children().children(".product-unitPrice").text();
            console.log("UNIT PRICE",unitPrice)
            if (products.length > 0 && products.findIndex(p => p.productId === idProduct) >= 0) {
                products.push({ productId: idProduct, UnitPrice: Number(unitPrice) });
                const quantityText = $(`.quantity-product-${idProduct}`);
                quantityText.text(Number(quantityText.text()) + 1);
            } else {
                quantity = 1;
                const titleProduct = $(this).parent().parent().children(".product-title").text();
                products.push({ productId: idProduct, UnitPrice: Number(unitPrice) });
                let productItem = `
                    <tr>
                       <td class="quantity-product-${idProduct}">${quantity}</td>
                       <td>${titleProduct}</td>
                   </tr>
                `;
                productsTable.append(productItem);
            }
            console.log(products)
            total = products.reduce((acc, curr) => acc + curr.UnitPrice, 0);
            totalProducts.text(total);
        })
    })
    btnCreateSale.on("click", function (e) {
        e.preventDefault();
        console.log("IS WORKING")

        if (products.length === 0 || client.val().trim() === "" || description.val().trim() === "" || phone.val().trim() === "") {
            errorMessage.text("all fields are required");
            return;
        }

        const formData = {
            SaleProducts: products,
            client: client.val(),
            description: description.val(),
            contact: phone.val(),
            totalPrice: total
        };

        $.ajax({
            url: "/Sale/Create",
            type: "POST",
            data: formData,
            success: function (response) {
                alert(response.message);
                location.href = "/Sale";
            },
            error: function (error) {
                console.log(error.message);
                location.href = "/Sale";
            }
        })
    })
});