using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.Exam_Entities
{
    public class Exam_AnswersheetModel
    {
        
        [Display(Name = "Q. No.")]
        public int qNo { get; set; }

        [Display(Name = "Question")]
        public string qText { get; set; }

        [Display(Name = "Answer")]
        public string ansText { get; set; }

        [Display(Name = "Question Type")]
        public string qNature { get; set; }

        [Display(Name = "Option a")]
        public string Oa { get; set; }

        [Display(Name = "Option b")]
        public string Ob { get; set; }

        [Display(Name = "Option c")]
        public string Oc { get; set; }

        [Display(Name = "Option d")]
        public string Od { get; set; }

        [Display(Name = "Q. Mark")]
        public decimal qMark { get; set; }

        [Display(Name = "Answer")]
        public string ans { get; set; }

        [Display(Name = "Correct answer")]
        public string correctAnswer { get; set; }

        [Display(Name = "Result")]
        public string ansResult { get; set; }

        [Display(Name = "Mark Scored")]
        public decimal markScored { get; set; }

        [Display(Name = "Mark Scored")]
        public string markScored1 { get; set; }


        public string QS { get; set; }

        public bool isActive { get; set; }
        public bool isHavingParagraph { get; set; }

        public string defaultbtnclick { get; set; }

        public string parValue { get; set; }

    }
    public class StudentAssessmentResult
    {
        public StudentAssessmentResult()
        {
            answersheetList = new List<Exam_AnswersheetModel>();
        }
        [Display(Name = "Subject")]
        public string SubjectDisplayName { get; set; }
        [Display(Name = "Assessment")]
        public string AssessmentName { get; set; }
        [Display(Name = "Max. Marks")]
        public int maxMark { get; set; }
        [Display(Name = "Result")]
        public string result { get; set; }
        [Display(Name = "Mark Obtained")]
        public decimal markObtained { get; set; }

        public List<Exam_AnswersheetModel> answersheetList { get; set; }
    }

    

}
