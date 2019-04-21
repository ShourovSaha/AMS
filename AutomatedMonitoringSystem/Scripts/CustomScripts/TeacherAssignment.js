function PeriodAssionWithTeachersSubject() {
    var SubjectId = $("#SubjectId").val();
    var ClassId = $("#ClassId").val();
    var SectionId = $("#SectionId").val();
    var Day = $("#Day").val();
    var PeriodId = $("#PeriodId").val();
    var UserPhone = $("#UserPhone").val();

    if (SubjectId == null || SubjectId == "undifined" || SubjectId == "" ||
        ClassId == null || ClassId == "undifined" || ClassId == "" ||
        SectionId == null || SectionId == "undifined" || SectionId == "" ||
        Day == null || Day == "undifined" || Day == "" 
         ||UserPhone == null || UserPhone == "undifined" || UserPhone == "") {
        alert("Input data correctly!");
        return 0;
    }
    else {

        $.ajax({
            method: "POST",
            url: "/User/PeriodAssignmentAjax",
            data: JSON.stringify({ 'subjectId': SubjectId, 'periodId': PeriodId, 'userPhone': UserPhone }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data == "Y") {
                    var Lang = localStorage.getItem("lang");
                    if (Lang == "bn") {
                        alert("ডাটা সংরক্ষিত হয়েছে |");
                    }
                    else {
                        alert("Data Saved Successfully.");
                    }
                    
                }
                else {
                    if (Lang == "bn") {
                        alert("একই ডাটা আগে থেকে সংরক্ষিত রয়েছে |");
                    }
                    else {
                        alert("Already Assigned!");
                    }
                    
                }
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
                //$("#dataTable").dataTable().fnDestory();
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
