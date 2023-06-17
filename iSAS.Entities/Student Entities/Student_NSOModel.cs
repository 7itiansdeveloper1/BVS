using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities
{
    public class Student_NSOModel
    {
        public Student_NSOModel()
        {
            DropDownList = new DropDownListFor_Student_NSO();
        }

        public string Mode { get; set; }

        [StringLength(10, ErrorMessage = "Max Len 10..")]
        public string SessionId { get; set; } //It was not on form view

        [Display(Name = "NSO No")]
        [StringLength(10, ErrorMessage = "Max Len 10..")]
        public string NSO_No { get; set; }

        [Required(ErrorMessage = "Applied Date is Req..!")]
        [Display(Name = "Applied Date")]
        [StringLength(30, ErrorMessage = "Max Len 10..")]
        public string NSO_AppliedDate { get; set; }

        [Display(Name = "ERP No")]
        [StringLength(10, ErrorMessage = "Max Len 10..")]
        public string ERPNO { get; set; }


        [Required(ErrorMessage = "Class is Req..!")]
        [Display(Name = "Class")]
        [StringLength(10, ErrorMessage = "Max Len 10..")]
        public string ClassId { get; set; }

        [Required(ErrorMessage = "Section is Req..!")]
        [Display(Name = "Section")]
        [StringLength(10, ErrorMessage = "Max Len 10..")]
        public string SectionId { get; set; }

        [Display(Name = "Reason for NSO")]
        [StringLength(50, ErrorMessage = "Max Len 50..")]
        public string NSO_Reason { get; set; }

        [Display(Name = "Refund Fee")]
        public int Refund_Amount { get; set; }

        [Display(Name = "Remarks")]
        [StringLength(200, ErrorMessage = "Max Len 200..")]
        public string NSO_Description { get; set; }


        public bool IsNSOCancelled { get; set; }

        //Extra Feild

        [Display(Name = "Admission No")]
        public string AdmissionNo { get; set; } // Not in Database

        [Required(ErrorMessage ="Student is Req..!")]
        [Display(Name = "Student")]
        public string StudentID { get; set; } //Not in Database

        [Display(Name = "Last NSO No")]
        public string LastNSONo { get; set; }

        [Display(Name = "Current Session NSO Count")]
        public int CurrentSessionNSOCount { get; set; }

        [Display(Name ="Student Name")]
        public string StudentName { get; set; }

        [Display(Name = "Father Name")]
        public string FatherName { get; set; }

        public DropDownListFor_Student_NSO DropDownList { get; set; }

        //public string CreatedBy { get; set; }
        //public string CreationDate { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
    }

    public class DropDownListFor_Student_NSO
    {
        public DropDownListFor_Student_NSO()
        {
            SessionList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            SectionList = new List<SelectListItem>();
            StudentList = new List<SelectListItem>();
        }

        public string LastNSONo { get; set; }
        public int CurrentSessionCount { get; set; }

        public List<SelectListItem> SessionList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> StudentList { get; set; }
    }
}
