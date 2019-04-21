
function ShowStudentAttendance() {

    if (InputValidation() == 1) {

        var Roll = $("#Roll").val();
        if (Roll == null || Roll == "undifined" || Roll == "") {
            GetAttandanceClassSactionWise();
        }
        else {
            GetAttandanceClassSactionRollWise();
        }
    }
    else {
        alert("Input data correctly!");
    }

}



function GetAttandanceClassSactionRollWise() {
    var studentAttendanceByRollVM = DataInitializationWithRoll();

    $.ajax({
        method: "POST",
        url: "/Attendance/StudentAttendanceClassSectionRollWise",
        data: JSON.stringify({ 'studentAttendanceByRollVM': studentAttendanceByRollVM }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data, response) {
            var html = "";
            if (data != null) {
                if (data[0] == null) {
                    alert("No data found!");
                    return;
                }
                //html += "<tr>";
                //html += "<th>" + "Name" + "</th>";
                //html += "<th>" + "Roll" + "</th>";
                //html += "<th>" + "Present Status" + "</th>";
                //html += "</tr>";
                //$('.table').find('thead').html(html);
                //html = "";
                $.each(data, function (key, value) {
                    html += "<tr>";
                    html += "<td>" + value.Name + "</td>";
                    html += "<td>" + value.Roll + "</td>";
                    html += "<td>" + value.PresentStatus + "</td>";
                    html += "</tr>";
                });
                
                $('.table').find('tbody').html(html);
                if (response != 'success') {
                    alert("No data found!");
                }
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}



function GetAttandanceClassSactionWise() {

    var studentAttendanceByClassVM = DataInitialization();
    $.ajax({
        method: "POST",
        url: "/Attendance/StudentAttendanceClassSectionWise",
        data: JSON.stringify({ 'studentAttendanceByClassVM': studentAttendanceByClassVM}),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data, response) {
            var html = "";
            if (data != null) {
                if (data[0] == null) {
                    alert("No data found!");
                    return;
                }
                //html += "<tr>";
                //html += "<th>" + "Name" + "</th>";
                //html += "<th>" + "Roll" + "</th>";
                //html += "<th>" + "PresentStatus" + "</th>";
                //html += "</tr>";
                //$('.table').find('thead').html(html);
                //html = "";
                $.each(data, function (key, value) {
                    html += "<tr>";
                    html += "<td>" + value.Name + "</td>";
                    html += "<td>" + value.Roll + "</td>";
                    html += "<td>" + value.PresentStatus + "</td>";
                    html += "</tr>";
                });
                
                $('.table').find('tbody').html(html);
                if (response != 'success') {
                    alert("No data found!");
                }
            }
        },
        error: function (err) {
            console.log(err);
        }
    });
}


function DataInitialization() {
    var ClassId = $("#ClassId").val();
    var SectionId = $("#SectionId").val();
    var Shift = $("#Shift").val();
    var Date = $("#Date").val();


    studentAttendanceByClassVM = {
        ClassId: ClassId,
        SectionId: SectionId,
        Shift: Shift,
        Date: Date
    }

    return studentAttendanceByClassVM;
}

function DataInitializationWithRoll() {
    var ClassId = $("#ClassId").val();
    var SectionId = $("#SectionId").val();
    var Shift = $("#Shift").val();
    var Date = $("#Date").val();
    var Roll = $("#Roll").val();

    studentAttendanceByRollVM = {
        ClassId: ClassId,
        SectionId: SectionId,
        Shift: Shift,
        Date: Date,
        Roll: Roll
    }

    return studentAttendanceByRollVM;
}


function InputValidation() {

    var ClassId = $("#ClassId").val();
    var SectionId = $("#SectionId").val();
    var Shift = $("#Shift").val();
    var Date = $("#Date").val();
    var Roll = $("#Roll").val();

    if (ClassId == null || ClassId == "undifined" || ClassId == "" ||
        SectionId == null || SectionId == "undifined" || SectionId == "" ||
        Shift == null || Shift == "undifined" || Shift == "" ||
        Date == null || Date == "undifined" || Date == "") {
        //alert("Input data correctly!");
        return 0;
    }
    return 1;
}