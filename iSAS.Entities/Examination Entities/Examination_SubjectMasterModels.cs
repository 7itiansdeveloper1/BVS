using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.Examination_Entities
{
    public class Examination_SubjectMasterModels
    {
        public Examination_SubjectMasterModels()
        {
            SubjectPrintOrderList = new List<SelectListItem>();
            SubjectList = new List<SelectListItem>();
        }

        public string TemplateId { get; set; }
        public string TempleteName { get; set; }

        [Required(ErrorMessage = "Subject Order is Req..!")]
        [Display(Name = "Subject Order")]
        public string SubjectPrintOrderId { get; set; }


        [Display(Name = "Subject Name")]
        public string SubjectId { get; set; }

        public bool chkboxAddNewSubject { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 Char is allowed..!")]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [StringLength(150, ErrorMessage = "Max 150 Char is allowed..!")]
        [Display(Name = "Display Name")]
        public string SubjectDisplayName { get; set; }

        public string TransactionMode { get; set; }
        public string UserId { get; set; }
        public bool IsActive { get; set; }
      //  public int PrintOrder { get; set; }


        public int SubjectPropertyId { get; set; }


        public string Assessment { get; set; }
        public bool IsDeleteable { get; set; }
        public bool HavingChildSubject { get; set; }

        public List<SelectListItem> SubjectPrintOrderList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
    }


    public class Examination_SubjectPropertyModel
    {
        //[StringLength(10)]
        public int SubjectPropertyId { get; set; }

        [StringLength(10)]
        public string ExamTemplateId { get; set; }
        public string Exam_TemplateName { get; set; }

        [StringLength(10)]
        public string SubjectId { get; set; }
            
        public bool HavingChildSubject { get; set; }
        public char SubjectType { get; set; }
        public bool IsMarkBased { get; set; }
        public bool IsGradeBased { get; set; }
        public bool IncludePracticalSubject { get; set; }
        public bool IsOptionalSubject { get; set; }
        public bool IsCompulsorySubject { get; set; }
        public bool IsAcademicSubject { get; set; }
        public bool IsNonAcademicSubject { get; set; }


        [StringLength(5)]
        public string ParentSubjectId { get; set; }
        public bool Is1stLanguage { get; set; }
        public bool Is2ndLanguage { get; set; }
        public bool Is3rdLanguage { get; set; }
        public bool DisplayOnReportCard { get; set; }
        public bool DispalyMark { get; set; }
        public bool DispalyGrade { get; set; }
        public int PrintOrder { get; set; }
        public string SubjectName { get; set; }
        public string SubjectDisplayName { get; set; }
        public string Assessment { get; set; }


        public string UserId { get; set; }
        public string TransactionMode { get; set; }
    }
}
