//document.addEventListener('contextmenu', event => event.preventDefault());
function getStateListByCountry(e, t, n) { var o = '<option value="">--Select--</option>'; $.ajax({ type: "GET", url: "../Common/GetStateListByCountryID", data: { CountryID: $("#" + e).val() }, success: function (e) { for (var a = 0; a < e.length; a++)o += "<option value=" + e[a].Value + ">" + e[a].Text + "</option>"; $("#" + t).html(o), $("#" + n).html('<option value="">--Select--</option>') } }) } function getCityListByState(e, t) { var n = '<option value="">--Select--</option>'; $.ajax({ type: "GET", url: "../Common/GetCityListByStateID", data: { StateID: $("#" + e).val() }, success: function (e) { for (var o = 0; o < e.length; o++)n += "<option value=" + e[o].Value + ">" + e[o].Text + "</option>"; $("#" + t).html(n) } }) } function getSectionListByClass(e, t) { var n = '<option value="">--Select--</option>'; $.ajax({ type: "GET", url: "../StudentAttendance/GetSectionsForClass", data: { classId: $("#" + e).val() }, success: function (e) { for (var o = 0; o < e.length; o++)n += "<option value=" + e[o].SecId + ">" + e[o].SecName + "</option>"; $("#" + t).html(n) } }) } function GetStudentListForTCByClassSection(e, t, n, o) { $.ajax({ type: "GET", url: "../Student_TC/GetddlStudentList", data: { sessionId: $("#" + e).val(), classId: $("#" + t).val(), sectionId: $("#" + n).val() }, success: function (e) { var t = $("#" + o); t.empty(), t.append($("<option />").text("--- Select Student ---")), $.each(e, function () { this.Student.indexOf("(NSO)") <= 0 ? t.append($("<option />").val(this.ERPNo).text(this.Student)) : t.append($("<option />").val(this.ERPNo).text(this.Student).prop("disabled", !0)) }), $("#ERPNO").val(""), $("#AdmissionNo").val(""), $("#FatherName").val("") } }) } function GetStudentListByClassSection(e, t, n, o) { var a = '<option value="">--Select--</option>'; $.ajax({ type: "GET", url: "../Common/GetStudentList", data: { sessionId: $("#" + e).val(), classId: $("#" + t).val(), sectionId: $("#" + n).val() }, success: function (e) { for (var t = 0; t < e.length; t++)a += "<option value=" + e[t].Value + ">" + e[t].Text + "</option>"; $("#" + o).html(a) } }) } function AddValidAttributeCSS(e) { if (void 0 !== e) for (var t = 0; t < e.length; t++)$("#" + e[t].Title).css("border-color", ""), $('span[data-valmsg-for="' + e[t].Title + '"]').text("") } function AddErrorAttributeCSS(e) { if (void 0 !== e) for (var t = 0; t < e.length; t++)$("#" + e[t].Title).css("border-color", "red"), $('span[data-valmsg-for="' + e[t].Title + '"]').text(e[t].Error); else OpenMessegeAutoHideDiv("Internal server error..!", "Danger") } function NoSpaceAtAnyPlace(e) { return 32 !== e.which || (OpenMessegeAutoHideDiv("Can not pass space in this text", "Info"), !1) } function NumericOnly(e) { var t = e.which ? e.which : enventkeyCode; return t >= 48 && t <= 57 } function NumericOnlyWithHyphin(e) { var t = e.which ? e.which : enventkeyCode; return t >= 48 && t <= 57 || 45 == t } function NumericDecimalOnly(e) { var t = e.which ? e.which : enventkeyCode; return 45 == t && -1 == $(this).val().indexOf("-") || 46 == t && -1 == $(this).val().indexOf(".") || !(t < 48 || t > 57) } function getRouteStopeByRouteId(e, t) { var n = '<option value="">--Select--</option>'; $.ajax({ type: "GET", url: "../Common/GetRouteStopByRouteId", data: { RouteId: $("#" + e).val() }, success: function (e) { for (var o = 0; o < e.length; o++)n += "<option value=" + e[o].Value + ">" + e[o].Text + "</option>"; $("#" + t).html(n) } }) } function OpenMessegeAutoHideDiv(e, t, n) { var o = "icon fa fa-info"; "" == e || null == e ? (e = "There is no response from your request..!", o = "icon fa fa-ban") : ("Success" == t ? o = "icon fa fa-check" : "Warning" == t ? o = "icon fa fa-warning" : "Danger" == t && (o = "icon fa fa-ban"), null != n && "" != n || (n = t)), null != t && "" != t ? $.notify({ icon: o, message: e }, { type: t.toLowerCase() }) : $.notify({ icon: o, message: e }, { type: "info" }) } function OpenMessegeModal(e, t, n, o) { var a = "#337ab7"; "" != e && null != e || (e = "There is no response from your request..!"), "" != t && null != t || (t = "Primary"), "" != n && null != n || (n = "sm"), "sm" == n ? $("#modalDialog").addClass("modal-sm") : "lg" == n ? $("#modalDialog").addClass("modal-lg") : ($("#modalDialog").removeClass("modal-lg"), $("#modalDialog").removeClass("modal-sm")), "Info" == t ? a = "#5bc0de" : "Success" == t ? a = "#5cb85c" : "Warning" == t ? a = "#f0ad4e" : "Danger" == t && (a = "#d9534f"), $("#modalTitle").html(o), $("#mainModalHeader").css("background-color", a), $("#modalbtnOK").css("background-color", a), $("#modalMsgBody").html(e), $("#mainModal").modal() } function OpenModalWithSave(e, t) { "" != e && null != e || (e = "There is no content to display..!"), $("#modalTitleWithSave").html(t), $("#modalMsgBodyWithSave").html(e), $("#mainModalWithSave").modal() } function OpenChatModal(e) { "" != e && null != e || (e = "There is no messege..!"), $("#mainModalChatBody").html(e), $("#mainModalChat").modal() } function modalBtnOkClick() { null != $("#hddnRedirectRoute").val() && "undefined" != $("#hddnRedirectRoute").val() && "" != $("#hddnRedirectRoute").val() && (window.location.href = $("#hddnRedirectRoute").val()) } function AsyncConfirmYesNo(e, t, n, o, a, i) { swal({ title: e, text: t, type: "warning", showCancelButton: !0, confirmButtonColor: "#3085d6", cancelButtonColor: "#d33", confirmButtonText: "Yes, do it!" }).then(e => e.value ? n(a) : o(a)) } function copyTextToClipboard(e) { var t = document.createElement("textarea"); t.value = e, document.body.appendChild(t), t.select(); try { var n = document.execCommand("copy") ? "successful" : "unsuccessful"; console.log("Copying text command was " + n) } catch (e) { console.log("Oops, unable to copy") } document.body.removeChild(t) } function fun_GetAttenCal(e) { var t = $(e).attr("id").split("/"); fun_loadAttendence(t[0], t[1], t[2]) } function fun_loadAttendence(e, t, n) { $.ajax({ url: "../Dashboard/AsyncUpdateCalender", type: "GET", traditional: !0, contentType: "application/json", data: { month: e, year: t, erpNo: n }, success: function (e) { $("#showAttendanceCalMainDiv").html(e) } }) } function fun_GetAttenCal_Staff(e) { var t = $(e).attr("id").split("/"); fun_loadAttendence_Staff(t[0], t[1]) } function fun_loadAttendence_Staff(e, t, n) { $.ajax({ url: "../DashBoard_StaffStatatics/AsyncUpdateCalender", beforeSend: function () { $("#spinner").show() }, type: "GET", traditional: !0, contentType: "application/json", data: { month: e, year: t }, success: function (e) { $("#staffAttnCalendarMainDiv").html(e) }, beforeSend: function () { $("#spinner").hide() } }) }


function Callclass1(classurl) {
    console.log(classurl);
    window.open(classurl, "_blank")
}

function PasstoModal(zoomurl,classguid) {
    
    $('#zoomurl').val(zoomurl);
    $('#classguid').val(classguid);
}
function joinclass(scode) {

    if ($('#scode').val() == scode) {
        Callclass1($('#zoomurl').val());
    }
    else {
        OpenMessegeAutoHideDiv("Incorrect secret code.", 'Danger');
        $('#btnmodelclose').click();
    }

    
    
}
function checkLogin(username,pwd) {
    $.ajax({
        type: "POST",
        beforeSend: function () {
            spinnerShow();
        },
        url: $form.attr('action'),
        data: $form.serialize(),
        error: function (xhr, status, error) {
            OpenMessegeAutoHideDiv($('#ErrorMsgOnJasonFailed').val(), 'Danger');
        },
        success: function (response) {
            if (response.status == 'success') {
                OpenMessegeAutoHideDiv(response.Msg, response.Color);
                getBankMasterDetails();
                emptyBankDetails();
            }
            else {
                OpenMessegeAutoHideDiv($('#ErrorMsgOnModelStateNotValid').val());
                AddErrorAttributeCSS(response.ErrorList); //Available in CustomCommon.js
                AddValidAttributeCSS(response.ValidKeyList); //Available in CustomCommon.js
            }
        },
        complete: function () {
            spinnerHide();
        }
    });
}

function spinnerShow() { $("#spinner").show() } function spinnerHide() { $("#spinner").hide() } function PrintReceipt_ByReceiptNo(e, t) { $.ajax({ type: "GET", beforeSend: function () { spinnerShow() }, url: "../StudentFeeDetails/GetFeeDocument_PrintOnly", data: { TransRefNo: e, Mode: "RECEIPT", erpno: t }, error: function (e, t, n) { OpenMessegeModal($("#ErrorMsgOnJasonFailed").val()) }, success: function (e) { "" != e && null != e && e.length() > 2 && OpenMessegeModal(e, "Info", "sm", "Messege") }, complete: function () { spinnerHide() } }) } function GetDaysBetweenDays(e, t, n) { var o = new Date($("#" + e).val().split("/").reverse().join("-")), a = new Date($("#" + t).val().split("/").reverse().join("-")), i = new Date(a - o) / 1e3 / 60 / 60 / 24; i + 1 > 0 ? $("#" + n).val(i + 1) : (OpenMessegeAutoHideDiv("In-Valid Date", "Warning"), $("#" + n).val(0)) }

