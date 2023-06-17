function getStateListByCountry(countryId, stateId, cityId) {
    var stateHtml = '<option value="">--Select--</option>';
    var cityHtml = '<option value="">--Select--</option>';
    $.ajax({
        type: "GET",
        url: "../Common/GetStateListByCountryID",
        data: { CountryID: $('#' + countryId).val() },
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                stateHtml += '<option value=' + result[i].Value + '>' + result[i].Text + '</option>';
            }
            $('#' + stateId).html(stateHtml);
            $('#' + cityId).html(cityHtml);
        },
    });
}

function getCityListByState(stateId, cityId) {
    var cityHtml = '<option value="">--Select--</option>';
    $.ajax({
        type: "GET",
        url: "../Common/GetCityListByStateID",
        data: { StateID: $('#' + stateId).val() },
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                cityHtml += '<option value=' + result[i].Value + '>' + result[i].Text + '</option>';
            }
            $('#' + cityId).html(cityHtml);
        },
    });
}
function getSectionListByClass(ClassId,SectionId) {
    var sectionHtml = '<option value="">--Select--</option>';
    $.ajax({
        type: "GET",
        url: "../StudentAttendance/GetSectionsForClass",
        data: { classId: $('#' + ClassId).val() },
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                sectionHtml += '<option value=' + result[i].SecId + '>' + result[i].SecName + '</option>';
            }
            $('#' + SectionId).html(sectionHtml);
        },
    });
}

function GetStudentListForTCByClassSection(sessionId, classId, sectionId, studentId)
{
    var studentHtml = '<option value="">--Select--</option>';
    $.ajax({
        type: "GET",
        url: "../Student_TC/GetddlStudentList",
        data: { sessionId: $('#' + sessionId).val(), classId: $('#' + classId).val(), sectionId: $('#' + sectionId).val() },
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                studentHtml += '<option value=' + result[i].ERPNo + '>' + result[i].Student + '</option>';
            }
            $('#' + studentId).html(studentHtml);

            $('#ERPNO').val('');
            $('#AdmissionNo').val('');
            $('#FatherName').val('');
        },
    });
}

function AddValidAttributeCSS(ValidAttributeList) {
    for (var i = 0; i < ValidAttributeList.length; i++) {
        $('#' + ValidAttributeList[i].Title).css('border-color', '');
        $('span[data-valmsg-for="' + ValidAttributeList[i].Title + '"]').text(''); //It wil add text to element
    }
}
function AddErrorAttributeCSS(ErrorList) {
    for (var i = 0; i < ErrorList.length; i++) {
        $('#' + ErrorList[i].Title).css('border-color', 'red');
        $('span[data-valmsg-for="' + ErrorList[i].Title + '"]').text(ErrorList[i].Error); //It wil add text to element
    }
}

function NumericOnly(evt) {
    var charCode = (evt.which) ? evt.which : enventkeyCode;
    if (charCode >= 48 && charCode <= 57) {
        return true;
    } else {
        return false;
    }
}

function getRouteStopeByRouteId(RouteId, StopId) {
    var stopHtml = '<option value="">--Select--</option>';
    $.ajax({
        type: "GET",
        url: "../Common/GetRouteStopByRouteId",
        data: { RouteId: $('#' + RouteId).val() },
        success: function (result) {
            for (var i = 0; i < result.length; i++) {
                stopHtml += '<option value=' + result[i].Value + '>' + result[i].Text + '</option>';
            }
            $('#' + StopId).html(stopHtml);
        },
    });
}

function OpenMessegeModal(msg, modalType, modalsize, modalTitle) {
    var backGroundColor = "#337ab7";
    if (msg == '' || msg == null) {
        msg = 'Default Msg';
    }
    if (modalType == '' || modalType == null) {
        modalType = 'Primary';
    }
    if (modalsize == '' || modalsize == null) {
        modalsize = 'sm';
    }

    if (modalsize == 'sm') {
        $('#modalDialog').addClass('modal-sm');
    } else if (modalsize == 'lg') {
        $('#modalDialog').addClass('modal-lg');
    }
    else {
        $('#modalDialog').removeClass('modal-lg');
        $('#modalDialog').removeClass('modal-sm');
    }

    if (modalType == 'Info') {
        backGroundColor = "#5bc0de";
    }
    else if (modalType == 'Success') {
        backGroundColor = "#5cb85c";
    }
    else if (modalType == 'Warning') {
        backGroundColor = "#f0ad4e";
    }
    else if (modalType == 'Danger') {
        backGroundColor = "#d9534f";
    }

    $('#modalTitle').html(modalTitle);
    $('#mainModalHeader').css('background-color', backGroundColor);
    $('#modalbtnOK').css('background-color', backGroundColor);
    $('#modalMsgBody').html(msg);
    $("#mainModal").modal();
}

function modalBtnOkClick()
{
    if ($('#hddnRedirectRoute').val() != null && $('#hddnRedirectRoute').val() != 'undefined' && $('#hddnRedirectRoute').val() != '') {
        window.location.href = $('#hddnRedirectRoute').val();
    }
}

function AsyncConfirmYesNo(title, msg, yesFn, noFn, evt) {
    var $confirm = $("#modalConfirmYesNo");
    $confirm.modal('show');
    $("#lblTitleConfirmYesNo").html(title);
    $("#lblMsgConfirmYesNo").html(msg);
    $("#btnYesConfirmYesNo").off('click').click(function () {
        yesFn(evt);
        $confirm.modal("hide");
    });
    $("#btnNoConfirmYesNo").off('click').click(function () {
        noFn();
        $confirm.modal("hide");
    });
}

function DatePickerSetting()
{
    $('.datefeild').attr('readonly', true);
    $('.datefeild').daterangepicker({
        singleDatePicker: true,
    }); 
}

function copyTextToClipboard(text) {
    var textArea = document.createElement("textarea");
    textArea.value = text;
    document.body.appendChild(textArea);
    textArea.select();
    try {
        var successful = document.execCommand('copy');
        var msg = successful ? 'successful' : 'unsuccessful';
        console.log('Copying text command was ' + msg);
    } catch (err) {
        console.log('Oops, unable to copy');
    }
    document.body.removeChild(textArea);
}

function fun_GetAttenCal(evt) {
    var object = $(evt).attr("id");
    var str = object.split('/');
    // str[0] contains "month"
    // str[1] contains "year"
    fun_loadAttendence(str[0], str[1]);
}

function fun_loadAttendence(month, year) {
    $.ajax
        ({
            url: '../Dashboard/AsyncUpdateCalender',
            type: 'GET',
            traditional: true,
            contentType: 'application/json',
            data: { month: month, year: year },
            success: function (result) {
                $('#showAttendanceCalMainDiv').html(result);
            },
        });
}