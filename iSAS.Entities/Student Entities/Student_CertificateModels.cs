using ISas.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Student_Entities
{
    public class Student_CertificateModels
    {
        public Student_CertificateModels()
        {
            ClassSecList = new List<SelectListItem>();
            StudentList = new List<SelectListItem>();
            CertificateTypeList = new List<SelectListItem>();
            SessionList = new List<SelectListItem>();
            SectionList = new List<SelectListItem>();
        }
        public string StudentCertificateId { get; set; }

        [Required(ErrorMessage = "Certificate Type is required..!")]
        [Display(Name = "Certificate Type")]
        public string CertificateID { get; set; }

        [Required(ErrorMessage = "Session Id is required..!")]
        [Display(Name = "Session Id")]
        public string SessionId { get; set; }

        [Required(ErrorMessage = "ERP No is required..!")]
        [Display(Name = "ERP No")]
        public string ERPNO { get; set; }

        [Required(ErrorMessage = "Applied Date is required..!")]
        [Display(Name = "Applied Date")]
        public string AppliedDate { get; set; }

        [Required(ErrorMessage = "Class is required..!")]
        [Display(Name = "Class")]
        public string ClassId { get; set; }

        [Required(ErrorMessage = "Section is required..!")]
        [Display(Name = "Section")]
        public string SectionId { get; set; }

        [Required(ErrorMessage = "Issue Date is required..!")]
        [Display(Name = "Issue Date")]
        public string IssueDate { get; set; }

        [Display(Name = "Last Exam Date")]
        public string LastExamDate { get; set; }

        [Display(Name = "Last Exam Given")]
        public string LastExamGiven { get; set; }

        [Required(ErrorMessage = "Remark is required..!")]
        [Display(Name = "Remark")]
        public string Remark { get; set; }
        public string UserId { get; set; }
        public string Mode { get; set; }

        [Required(ErrorMessage = "Adm No is required..!")]
        [Display(Name = "Adm No")]
        public string AdmissionNo { get; set; }
        // public string CertificateType { get; set; }

        [Required(ErrorMessage = "Student is required..!")]
        [Display(Name = "Student")]
        public string StudentID { get; set; }

        public string SessId { get; set; }
        public string Student { get; set; }
        public string CertificateType { get; set; }

        public List<SelectListItem> ClassSecList { get; set; }
        public List<SelectListItem> StudentList { get; set; }
        public List<SelectListItem> CertificateTypeList { get; set; }
        public List<SelectListItem> SessionList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
    }
}
