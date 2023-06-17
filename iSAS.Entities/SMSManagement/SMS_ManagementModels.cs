using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.SMSManagement
{
    public class SMS_MainModel
    {
        public SMS_MainModel()
        {
            AdminAndStaffList = new List<SMS_AdminAndStaffModel>();
            StudentList = new List<SMS_StudentModel>();
            OutBoxList = new List<SMS_OutboxModel>();
            //referenceList = new List<SMS_ReferenceListModel>();
            MsgTypeList = new List<SelectListItem>();
            MsgTypeList.Add(new SelectListItem { Text = "SMS ONLY", Value = "SMS", Selected = true });
            MsgTypeList.Add(new SelectListItem { Text = "ALERT ON APP ONLY", Value = "SMSALERT" });
            MsgTypeList.Add(new SelectListItem { Text = "BOTH", Value = "BOTH" });
        }

        [Display(Name = "SMS Text")]
        public string SMSText { get; set; }

        [Display(Name = "SMS Text")]
        public string SMSText_Staff { get; set; }

        public string MsgType_Stud { get; set; }
        public string MsgType_Staff { get; set; }

        public string PostStudentSMSGroupType { get; set; }
        public string PostStaffSMSGroupType { get; set; }

        public string StudentSMSFilterType { get; set; } //ClassWise//SMSGroupWise
        public string StaffSMSFilterType { get; set; }  //DepartmentWise //SMSGroupWise // AdminStaff

        [Display(Name = "Student Group")]
        public string StudentGroupId { get; set; }

        [Display(Name = "Class")]
        public string ClassWithSectionIDs { get; set; }

        [Display(Name = "Staff Group")]
        public string StaffGroupId { get; set; }

        [Display(Name = "SMS Group Name")]
        public string SMSStudentGroupName { get; set; }

        [Display(Name = "SMS Group Name")]
        public string SMSStaffGroupName { get; set; }

        public DropDownListFor_SMSManagement DropDownList { get; set; }

        public List<SMS_AdminAndStaffModel> AdminAndStaffList { get; set; }
        public List<SMS_StudentModel> StudentList { get; set; }
        public List<SMS_OutboxModel> OutBoxList { get; set; }
        public List<SelectListItem> MsgTypeList { get; set; }
       // public List<SMS_ReferenceListModel> referenceList { get; set; }
    }

    public class DropDownListFor_SMSManagement
    {
        public DropDownListFor_SMSManagement()
        {
            ClassList = new List<SelectListItem>();
            DepartmentList = new List<SelectListItem>();
            StudentGroupList = new List<SelectListItem>();
            TeacherGroupList = new List<SelectListItem>();
        }

        [Display(Name = "Credit Balance")]
        public string CreditBalance { get; set; }

        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> StudentGroupList { get; set; }
        public List<SelectListItem> TeacherGroupList { get; set; }
    }
    public class SMS_AdminAndStaffModel
    {
        public bool IsSelected { get; set; }
        public string StaffID { get; set; }
        public string StaffCode { get; set; }
        public string StaffName { get; set; }
        public string Department { get; set; }
        public string Mobile { get; set; }
        //public string ModelType { get; set; }
        public string SubGuidId { get; set; }
    }
    public class SMS_StudentModel
    {
        public bool IsSelected { get; set; }
        public string ERP { get; set; }
        public string AdmNo { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string SMSNo { get; set; }
        public string Father { get; set; }
        public string SubGuidId { get; set; }
    }
   
    public class SMS_OutboxModel
    {
        public bool Selected { get; set; }
        public string MobileNo { get; set; }
        public string SMSText { get; set; }
        public string ReciverName { get; set; }
        public string RecordSendBy { get; set; }
        public DateTime RecordSendDate { get; set; }
        public DateTime SendDate { get; set; }

        public int SendSMS_Status { get; set; } //0 for not send, 1 send sms successfully
    }
    public class SMS_ResponseModel
    {
        public SMS_ResponseModel()
        {
            data = new List<SMS_ResponseDetailModel>();
        }
        public string msgid { get; set; }
        public string message { get; set; }
        public string status { get; set; }
        public List<SMS_ResponseDetailModel> data { get; set; }
    }
    public class SMS_ResponseDetailModel
    {
        public string id { get; set; }
        public string mobile { get; set; }
        public string status { get; set; }
        public string erpno { get; set; }
        public string status_desc { get; set; }
        public DateTime? delivered_date { get; set; }
    }
}
