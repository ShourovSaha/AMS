function PeriodAssionWithTeachersSubject() {
    var SubjectId = $("#SubjectId").val();
    var ClassId = $("#ClassId").val();
    var SectionId = $("#SectionId").val();
    var Day = $("#Day").val();
    var PeriodId = $("#PeriodId").val();
    //var UserPhone = $("#UserPhone").val();

    if (SubjectId == null || SubjectId == "undifined" || SubjectId == "" ||
        ClassId == null || ClassId == "undifined" || ClassId == "" ||
        SectionId == null || SectionId == "undifined" || SectionId == "" ||
        Day == null || Day == "undifined" || Day == "" 
        /* ||UserPhone == null || UserPhone == "undifined" || UserPhone == ""*/) {
        alert("Input data correctly!");
        return 0;
    }
    else {

        $.ajax({
            method: "POST",
            url: "/User/PeriodAssignmentAjax",
            data: JSON.stringify({ 'subjectId': SubjectId, 'periodId': PeriodId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                alert(data);
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
}







$('#SubjectId').change(function () {
    var SubjectId = $("#SubjectId").val();

    if (SubjectId == null || SubjectId == "undifined" || SubjectId == "") {
        alert("Input data correctly!");
        return 0;
    }
    else {
        
        $.ajax({
            method: "POST",
            url: "/User/GetTeachersBySubject",
            data: JSON.stringify({ 'subjectId': SubjectId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                //$("#dataTable").dataTable().clear().draw();
                $("#dataTable").dataTable({
                    data: data,
                    columns: [
                        { 'data': 'UId' },
                        { 'data': 'FirstName' },
                        { 'data': 'LastName' },
                        { 'data': 'Gender' },
                        { 'data': 'Email' }
                    ]
                });
            },
            error: function (err) {
                console.log(err);
            }
        });
    }
});


//function GetAllTeacher() {
//    $.ajax({
//        method: "POST",
//        url: "/User/GetTeachersAjax",
//        data: JSON.stringify({ 'subjectId': SubjectId }),
//        dataType: "json",
//        contentType: "application/json; charset=utf-8",
//        success: function (data) {
//            //$("#dataTable").dataTable().clear().draw();
//            $("#dataTable").dataTable({
//                data: data,
//                columns: [
//                    { 'data': 'UId' },
//                    { 'data': 'FirstName' },
//                    { 'data': 'LastName' },
//                    { 'data': 'Gender' },
//                    { 'data': 'Email' }
//                ]
//            });
//        },
//        error: function (err) {
//            console.log(err);
//        }
//    });

//}
