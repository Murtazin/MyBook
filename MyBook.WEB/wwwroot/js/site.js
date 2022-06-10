// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function AddDeleteFavorites(actionUrl)
{
    $(document).ready(function() {
        $.ajax({
            type: "POST",
            url: actionUrl,
            dataType: "json",
            contentType: 'application/json; charset=utf-8',
            success: function(data) {
                $('#modalText').text(data.msg);
            },
            error: function() {
                $('#modalText').text("Ошибка");
            }
        });
    });
}

$(function() {
    $('#inputGroupSelect01').change(function() {
        sessionStorage.setItem('searchSelect', this.value);
    });
    if(sessionStorage.getItem('searchSelect')){
        $('#inputGroupSelect01').val(sessionStorage.getItem('searchSelect'));
    }

    $('#searchKeyword').change(function() {
        sessionStorage.setItem('searchKeyword', this.value);
    });
    if(sessionStorage.getItem('searchKeyword')){
        $('#searchKeyword').val(sessionStorage.getItem('searchKeyword'));
    }
});