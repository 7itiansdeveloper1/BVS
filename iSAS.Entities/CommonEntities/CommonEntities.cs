using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities
{
    public class StudentSearchModel
    {
        public StudentSearchModel()
        {
            SessionList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            SectionList = new List<SelectListItem>();
            StudentList = new List<SelectListItem>();
            SiblingList = new List<SelectListItem>();
        }

        [Display(Name = "Session")]
        public string SessionId { get; set; }

        [Display(Name = "Adm. No")]
        public string AdmNo { get; set; }

        [Display(Name = "ERP No")]
        public string ERPNo { get; set; }

        [Display(Name = "Class")]
        public string ClassId { get; set; }

        [Display(Name = "Section")]
        public string SectionId { get; set; }

        [Display(Name = "Student")]
        public string StudentId { get; set; }

        [Display(Name = "Father")]
        public string FatherName { get; set; }

        [Display(Name = "Mother")]
        public string MotherName { get; set; }

        [Display(Name = "Mobile")]
        public string MobileNo { get; set; }

        [Display(Name = "Fee Category")]
        public string FeeCategory { get; set; }

        public string FeeCategoryId { get; set; }

        public string SiblingName { get; set; }

        public List<SelectListItem> SessionList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> StudentList { get; set; }
        public List<SelectListItem> SiblingList { get; set; }
    }
    public class StudentSearchByPropertyModel
    {
        public int selectedRow { get; set; }
        public string SearchType { get; set; }
        public string SelectedClassID { get; set; }
        public string SearchText { get; set; }
        public List<SelectListItem> ClassList { get; set; }
    }

    
    public class ExceptionLogger
    {
        public string ExceptionMsg { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ExceptionStackTrace { get; set; }
    }
    public class UserHubModels
    {
        public string UserName { get; set; }
        public string UserRole { get; set; }
        public HashSet<string> ConnectionIds { get; set; }
    }

    public class Basic_StudentInfoModel
    {
        public bool Selected { get; set; }
        public string ERP { get; set; }
        public string DOA { get; set; }
        public string AdmNo { get; set; }
        public int RollNo { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string Address { get; set; }
        public string FMobileNo { get; set; }
        public string MMobileNo { get; set; }
        public string SMSNo { get; set; }
        public string AlternateNumber { get; set; }
        public string StructName { get; set; }
        public string OnlyClassName { get; set; }


    }
}
