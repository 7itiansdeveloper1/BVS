using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.SMSManagement
{
    public class SMSTempleteModel
    {
        [StringLength(10)]
        public string SMSID { get; set; }

        [Required(ErrorMessage = "Templete is Req..")]
        [StringLength(10)]
        public string TemplateID { get; set; }

        [Required(ErrorMessage = "SMS text is req..")]
        [StringLength(2000)]
        public string SmsText { get; set; }

        public bool Active { get; set; }

        [StringLength(10)]
        public string CreatedBy { get; set; }
        //public string CreationDate { get; set; }
        //public string ModifiedBy { get; set; }
        //public string ModifiedDate { get; set; }

        //Extra Feild
        public string Function { get; set; }

        public List<SelectListItem>  TempleteTypeList { get; set; }

        public string TemplateName { get; set; }
    }

    public class SMS_DropMessagesPageModel
    {
        public SMS_DropMessagesPageModel()
        {
            ClassList = new List<SelectListItem>();
            TempleteSMSList = new List<SelectListItem>();
        }

        public string MobileNos { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> TempleteSMSList { get; set; }

        public string SMSID { get; set; }
        public string SelectedFullSMSText { get; set; }

        public bool IsSharewithCoordinator { get; set; }
        public bool IsSharewithClassTeacher { get; set; }

        public bool IsEmailToCoordinator { get; set; }
        public bool IsEmailToClassTeacher { get; set; }
    }


    public class TeacherSMSPageModel
    {
        public TeacherSMSPageModel()
        {
            UnDeliveredSMSList = new List<UnDeliveredSMSModel>();
        }

        public string SelectedSMSID { get; set; }
        public string SelectedSMSText { get; set; }
        public List<UnDeliveredSMSModel> UnDeliveredSMSList { get; set; }
    }

    public class UnDeliveredSMSModel
    {
        public string MessageId { get; set; }
        public string ReportName { get; set; }
        public string ShortSMS { get; set; }
        public string FullMessage { get; set; }
    }

    public class UnDeliveredSMSDetailsModel
    {
        public System.Int64 Sno { get; set; }
        public string ERPNo { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string Mobile { get; set; }
    }

    public class SMS_DropMessages_CRUDModel
    {
        [Required]
        public string ERPNos { get; set; }

        public bool IsSharewithCoordinator { get; set; }
        public bool IsSharewithClassTeacher { get; set; }

        public bool IsEmailToCoordinator { get; set; }
        public bool IsEmailToClassTeacher { get; set; }

        [StringLength(10)]
        [Required]
        public string MessageId { get; set; }

        [StringLength(10)]
        public string UserId { get; set; }
    }
}
