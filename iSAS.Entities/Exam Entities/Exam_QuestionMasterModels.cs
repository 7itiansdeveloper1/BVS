using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.Exam_Entities
{
    public class Exam_QuestionMasterModels
    {
        //public Exam_QuestionMasterModels()
        //{
        //    //questionList = new List<Exam_QuestionList>();
        //}

        //[StringLength(200, ErrorMessage = "Only 200 Char. is Allowed..!")]
        //[Display(Name = "School Name")]
        //[Required(ErrorMessage = "School is Req..!")]
        

        //[Display(Name = "Alias")]
        

        //[Display(Name = "Code")]
        

        //[Required(ErrorMessage = "Address is Req..!")]

        [Display(Name = "Class")]
        public string className { get; set; }

        [Display(Name = "Subject")]
        public string subjectName { get; set; }

        [Display(Name = "Assessment")]
        public string assessmentName { get; set; }

        [Display(Name = "Max. Mark")]
        public int maxMark { get; set; }

        
        public int qpId { get; set; }
        public int qId { get; set; }

        [Required(ErrorMessage = "Q. No is Req..!")]
        [Display(Name = "Q. No.")]
        public int qNo { get; set; }

        [Required(ErrorMessage = "Q. Text is Req..!")]
        [Display(Name = "Question")]
        public string qText { get; set; }

        [Display(Name = "Answer")]
        public string ansText { get; set; }

        [Required(ErrorMessage = "Q. Type is Req..!")]
        [Display(Name = "Question Type")]
        public string qNature { get; set; }

        //[Required(ErrorMessage = "Optiona a is Req..!")]
        [Display(Name = "Option a")]
        public string Oa { get; set; }

        [Display(Name = "Option b")]
        //[Required(ErrorMessage = "Optiona b is Req..!")]
        public string Ob { get; set; }

        [Display(Name = "Option c")]
        //[Required(ErrorMessage = "Optiona c is Req..!")]
        public string Oc { get; set; }

        [Display(Name = "Option d")]
        //[Required(ErrorMessage = "Optiona d is Req..!")]
        public string Od { get; set; }

        [Display(Name = "Correct Answer")]
        //[Required(ErrorMessage = "Correct Answer is Req..!")]
        public string ans { get; set; }

        [Display(Name = "Q. Mark")]
        [Required(ErrorMessage = "Mark a is Req..!")]
        public decimal qMark { get; set; }

        public string QS { get; set; }

        public bool isActive { get; set; }
        public bool isHavingParagraph { get; set; }

        public string defaultbtnclick { get; set; }

        public string parValue { get; set; }

        //List<Exam_QuestionList> questionList = new List<Exam_QuestionList>();
    }
    //public class Exam_QuestionList
    //{
    //    public int qpId { get; set; }
    //    public int qId { get; set; }
    //    public int qNo { get; set; }
    //    public string qText { get; set; }
    //    public string qNature { get; set; }
    //    public string Oa { get; set; }
    //    public string Ob { get; set; }
    //    public string Oc { get; set; }
    //    public string Od { get; set; }
    //    public string ans { get; set; }
    //    public int qMark { get; set; }
    //    public bool isActive { get; set; }
    //}
}
