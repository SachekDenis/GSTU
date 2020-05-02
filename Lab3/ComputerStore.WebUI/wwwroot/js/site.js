// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function generateFieldInputs() {
    $.ajax({
        type: "GET",
        url: "/Products/SelectFieldsForCategory",
        dataType: "html",
        data: { categoryId: $("#categoryList").val() },
        success: (data) => {
            $("#fieldInputs").html(data);
        }
    });
}

function decimalValidation() {
    $.validator.methods.range = function(value, element, param) {
        const globalizedValue = value.replace(",", ".");
        return this.optional(element) || (globalizedValue >= param[0] && globalizedValue <= param[1]);
    };

    $.validator.methods.number = function(value, element) {
        return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:[\s,]\d{3})+)(?:[,]\d+)?$/.test(value);
    };
}