function AssignStudentWithGuardian(StudentId) {

    var UserPhone = $("#UserPhone").val();

    if (UserPhone == null || UserPhone == "undifined" || UserPhone == "") {
        alert("Fillup all the input fields!");
    }
    else {
        $.ajax({
            type: "POST",
            url: "/User/FinalAssignUserWithStudent",
            data: JSON.stringify({ 'UserPhone': UserPhone, 'StudentId': StudentId }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data, response) {

                var html = "";
                alert(data);
                //if (data != null) {
                    
                //    //$('#patient_ID').text(data.ID);
                //}
            },
            error: function (ex) {
                console.log(ex);
            }
        });

    }
}