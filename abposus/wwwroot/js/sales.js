$(document).ready(function () {
    const totalProducts = $("#total-products");
    let productsTable = $(".productsAdded");
    let products = [];
    $(".btnAddSale").each(function () {
        $(this).on("click", function (event) {
            event.preventDefault();
            const idProduct = $(this).data("id");
            let quantity = 0;
            const unitPrice = $(this).parent().parent().children(".product-unitPrice").text().slice(1);
            console.log("UNIT PRICE",unitPrice)
            if (products.length > 0 && products.findIndex(p => p.ProductId === idProduct) >= 0) {
                products.push({ ProductId: idProduct, UnitPrice: Number(unitPrice) });
                const quantityText = $(`.quantity-product-${idProduct}`);
                quantityText.text(Number(quantityText.text()) + 1);
            } else {
                quantity = 1;
                const titleProduct = $(this).parent().parent().children(".product-title").text();
                products.push({ ProductId: idProduct, UnitPrice: Number(unitPrice) });
                let productItem = `
                    <tr>
                       <td class="quantity-product-${idProduct}">${quantity}</td>
                       <td>${titleProduct}</td>
                   </tr>
                `;
                productsTable.append(productItem);
            }
            console.log(products)
            const total = products.reduce((acc, curr) => acc + curr.UnitPrice, 0);
            console.log(total)
            totalProducts.text(total);
        })
    })
});