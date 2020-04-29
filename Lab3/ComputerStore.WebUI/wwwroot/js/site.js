// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function generateFieldInputs() {
    $.ajax({
        type: "GET",
        url: "/Products/SelectFieldsForCategory",
        dataType: "html",
        data: { categoryId: $('#categoryList').val() },
        success: (data) => {
            $('#fieldInputs').html(data);
        }
    });
}