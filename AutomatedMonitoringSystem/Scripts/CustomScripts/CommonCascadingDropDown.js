// Cascading DropdownList for selecting Subject 

//for Subject while class then section is changed
$('#SectionId').change(function () {
    $.get('/Base/GetSubjectListByClassSection', { ClassId: $('#ClassId').val(), SectionId: $('#SectionId').val() }, function (data) {
        $('#SubjectId').empty();
        $('#SubjectId').append("<option value='" + -1 + "'>" + "--Select One--" + "</option>");
        $.each(data, function (key, value) {
            $('#SubjectId').append("<option value='" + value.SubjectId + "'>" + value.SubjectName + "</option>");
        });
    });
});


//for PeriodId while Day is changed
$('#Day').change(function () {
    $.get('/Base/GetPeriodsByWeekDay', { Day: $('#Day').val() }, function (data) {
        $('#PeriodId').empty();
        $('#PeriodId').append("<option value='" + -1 + "'>" + "--Select One--" + "</option>");
        $.each(data, function (key, value) {
            $('#PeriodId').append("<option value='" + value.PeriodId + "'>" + value.PeriodTime + "</option>");
        });
    });
});