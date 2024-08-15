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
                       <td>
                           <i role="button" class="bi bi-x-lg p-1 text-danger rounded-circle remove-${idProduct}"></i>
                       </td>
                   </tr>
                `;
                productsTable.append(productItem);

                $(`.remove-${idProduct}`).on("click", function () {
                    const quantity = ($(`.quantity-product-${idProduct}`));
                    const product = products.find(p => p.productId === idProduct);
                    const index = products.indexOf(product);
                    if (Number(quantity.text()) === 1) {
                        products.splice(index, 1);
                        quantity.text(Number(quantity.text()) - 1);
                        quantity.parent().remove();
                        console.log("REMOVING THIS ELEMENT PLEASE")
                        console.log(products);
                    } else {
                        products.splice(index, 1);
                        quantity.text(Number(quantity.text()) - 1);
                        console.log(products);
                    }
                })
            }
            total = products.reduce((acc, curr) => acc + curr.UnitPrice, 0);
            console.log(total);
            totalProducts.text(total.toFixed(2));
        })
    })
    btnCreateSale.on("click", function (e) {
        e.preventDefault();
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
                window.location.href = '/Sale'
            },
            error: function (error) {
                console.log(error.message);
                window.location.href = '/Sale'
            }
        })
    })
});