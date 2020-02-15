var container = $('#result-container');

$("#search-product").submit(function (e) {
    e.preventDefault();
    var form = $(this);
    searchProduct(form.serialize());
});

$("#purchase-details").submit(function (e) {
    e.preventDefault();
    var form = $(this);
    searchPurchaseDetails(form.serialize());
});

function searchProduct(formData) {
    $.ajax({
        url: '/api/Products',
        contentType: "application/json",
        data: formData,
        dataType: 'json',
        success: function (result) {
            writeProductResult(result.Data);
                console.clear();
                console.log(result);
        }
    });
}

function searchPurchaseDetails(formData) {
    $.ajax({
        url: '/api/PurchaseOrders',
        contentType: "application/json",
        data: formData,
        dataType: 'json',
        success: function (result) {
            writePurchaseOrderDetails(result.Data);
                console.clear();
                console.log(result);
        }
    });
}

function writeProductResult(result) {
    var table = document.createElement('table');
    table.className = 'table table-sm';
    var tableBody = document.createElement('tbody');
    var tableHead = document.createElement('thead');
    tableHead.className = 'thead-dark';
    tableHead.innerHTML = '<th>Product ID<th/><th>Product Number<th/><th>Product Name<th/><th>Sell Start Date<th/><th>Description<th/><th>Description Culture<th/>';
    table.appendChild(tableHead);
    for (i = 0; i < result.length; i++) {
        var row = document.createElement('tr');
        var item = result[i];
        row.innerHTML =
            '<td>' + item.ProductID + '<td/>' +
            '<td>' + item.ProductNumber + '<td/>' +
            '<td>' + item.ProductName + '<td/>' +
            '<td>' + item.SellStartDate + '<td/>' +
            '<td>' + item.Description + '<td/>' +
            '<td>' + item.DescriptionCulture + '<td/>';
        tableBody.appendChild(row);
    }

    table.appendChild(tableBody);
    container.html(table);
}

function writePurchaseOrderDetails(result) {
    var table = document.createElement('table');
    table.className = 'table table-sm';
    var tableBody = document.createElement('tbody');
    var tableHead = document.createElement('thead');
    tableHead.className = 'thead-dark';
    tableHead.innerHTML = '<th>Line Total Sum<th/><th>Order Qty Sum<th/><th>Due Date<th/>';
    table.appendChild(tableHead);
    for (i = 0; i < result.length; i++) {
        var row = document.createElement('tr');
        var item = result[i];
        row.innerHTML =
            '<td>' + item.LineTotalSum + '<td/>' +
            '<td>' + item.OrderQtySum + '<td/>' +
            '<td>' + item.DueDate + '<td/>';
        tableBody.appendChild(row);
    }

    table.appendChild(tableBody);
    container.html(table);
}