//function myFunction() {

//    var updatedValuesForOrder = {
//        "size": $("#SizeDropDownList option:selected").val(),
//        "crust": $("#CrustDropDownList option:selected").val(),
//        "green_peppers": $("#GreenPeppersCheckBox").is(":checked"),
//        "onions": $("#OnionsCheckBox").is(":checked"),
//        "pepperoni": $("#PepperoniCheckBox").is(":checked"),
//        "sausage": $("#SausagesCheckBox").is(":checked")
//    };

//    $.ajax({
//        url: '@Url.Action("UpdatePrice", "Home")',
//        type: "POST",
//        dataType: "json",
//        data: JSON.stringify(updatedValuesForOrder),
//        contentType: "application/json; charset=utf-8",
//        success: function (resp) {
//            $("#costOfOrder").html("The total cost for your order is:R" + resp);
//        }
//    });
//}