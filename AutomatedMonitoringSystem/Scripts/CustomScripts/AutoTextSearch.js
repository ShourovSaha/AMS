function SearchText() {
    $("#UserPhone").autocomplete({
        source: function (request, response) {
            $.ajax({
                type: "POST",
                url: "/User/GetGardiansPhoneNumber",
                data: JSON.stringify({ 'UserPhone': document.getElementById('UserPhone').value }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    response(data.value);
                },
                error: function (ex) {
                    console.log(ex);
                }
            });
        }
    });
}  