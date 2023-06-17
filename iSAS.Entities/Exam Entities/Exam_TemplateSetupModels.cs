using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.Exam_Entities
{
    public class Exam_TemplateSetupModels
    {
        [StringLength(10)]
        public string TemplateId { get; set; }

        [Display(Name = "Template Name")]
        [Required(ErrorMessage = "Templete Name is Req..")]
        [StringLength(50, ErrorMessage = "Max 50 char. is allowed..!")]
        public string TemplateName { get; set; }

        [Display(Name = "Display Name")]
        [Required(ErrorMessage = "Display Name is Req..")]
        [StringLength(50, ErrorMessage = "Max 50 char. is allowed..!")]
        public string Exam_TemplateDisplayName { get; set; }

        [Display(Name = "Print Order")]
        public Int16 PrintOrder { get; set; }

        [Display(Name = "Start Date")]
        public string StartDate { get; set; }

        [Display(Name = "End Date")]
        public string EndDate { get; set; }

        [Display(Name = "Result Date")]
        public string ResultDate { get; set; }

        [Display(Name = "Marks Feeding Last Date")]
        public string FeedingLastDate { get; set; }


        public string Classes { get; set; }
        public bool IsDeleteable { get; set; }

        public string UserId { get; set; }
        public string TransactionMode { get; set; }
    }


    public class Exam_Template_ClassSetupModels
    {
        public Exam_Template_ClassSetupModels()
        {
            ClassList = new List<SelectListItem>();
        }

        public string ExamTemplateId { get; set; }
        public string UserId { get; set; }
        public List<SelectListItem> ClassList { get; set; }
    }

    public class Exam_Template_GradingSetupModels
    {
        public Exam_Template_GradingSetupModels()
        {
            AcademicList = new List<SelectListItem>();
            NonAcademicList = new List<SelectListItem>();
            ReportCardList = new List<SelectListItem>();
        }

        [Display(Name ="Academic")]
        public string AcademicId { get; set; }

        [Display(Name = "Non-Academic")]
        public string NonAcademicId { get; set; }

        [Display(Name = "Report Card")]
        public string ReportCardId { get; set; }

        public string ExamTemplateId { get; set; }
        public string UserId { get; set; }
        // public string TransactionMode { get; set; }

        public List<SelectListItem> AcademicList { get; set; }
        public List<SelectListItem> NonAcademicList { get; set; }
        public List<SelectListItem> ReportCardList { get; set; }
    }
}
