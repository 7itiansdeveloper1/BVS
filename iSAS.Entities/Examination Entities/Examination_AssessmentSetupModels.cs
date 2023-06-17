using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Examination_Entities
{
    public class Examination_AssessmentSetupModels
    {
        public Examination_AssessmentSetupModels()
        {
            AssessmentNameList = new List<SelectListItem>();
        }

        public int AssessmentPropertyId { get; set; }

        [StringLength(10)]
        public string ExamTemplateId { get; set; }

        [StringLength(10)]
        public string Exam_TemplateName { get; set; }
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public int PrintOrder { get; set; }

        [StringLength(2000)]
        public string AssessmentId { get; set; }

        [Display(Name = "Assessment Name")]
        [StringLength(150)]
        public string AssessmentName { get; set; }

        [Display(Name = "Display Name")]
        [StringLength(150)]
        public string AssessmentDisplayName { get; set; }
      //  public string Assessment { get; set; }
        public bool IsDeleteable { get; set; }

        public bool IsActive { get; set; }
        public string TransactionMode { get; set; }

        [StringLength(10)]
        public string UserId { get; set; }

        public bool ChkBoxAddNewAssessment { get; set; }

        public List<SelectListItem> AssessmentNameList { get; set; }


        //Thse Three Properties are used while Assessment Setup for child subject..!
        public string MainSubjectId { get; set; }
        public string MainSubjectName { get; set; }
        public bool IsChildSubjectSetup { get; set; }
    }

    public class AssessmentPropertySetupModel
    {
        //[StringLength(10)]
        public int AssessmentPropertyId { get; set; }

        [StringLength(10)]
        public string ExamTemplateId { get; set; }
        public string Exam_TemplateName { get; set; }

        [StringLength(10)]
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public string AssessmentDisplayName { get; set; }

        [StringLength(1)]
        public string AssessmentNature { get; set; }

        public string AssessmentDate { get; set; }
        public string MarkFeedingLastDate { get; set; }
        public bool IsMarksFeedingon { get; set; }
        public int MaxMark { get; set; }
        public int PassingMark { get; set; }
        public int MaxMarkWeightage { get; set; }
        public int PrintOrder { get; set; }
        public string Assessment { get; set; }

        public string UserId { get; set; }
        public string TransactionMode { get; set; }
    }
}
