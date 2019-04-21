$('#SectionId').change(function () {
    var SectionId = $("#SectionId").val();
    var ClassId = $("#ClassId").val();
    if (SectionId == null || SectionId == "undifined" || SectionId == "" ||
        ClassId == null || ClassId == "undifined" || ClassId == "") {
        alert("Input data correctly!");
        return 0;
    }
    else {

        $.ajax({
            method: "POST",
            url: "/Student/GetStudentsByClassSection",
            data: JSON.stringify({ 'ClassId': ClassId, 'SectionId': SectionId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                //$("#dataTable").dataTable().clear().draw();
                $("#dataTable").dataTable({
                    data: data,
                    columns: [
                        { 'data': 'StudentId' },
                        { 'data': 'Name' },
                        { 'data': 'Roll' },
                    ]
                });
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
});
