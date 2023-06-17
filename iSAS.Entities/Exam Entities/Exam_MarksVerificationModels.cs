using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities
{
    public class Exam_MarksVerificationModels
    {
        public Exam_MarksVerificationModels()
        {
            SessionList = new List<SelectListItem>();
            ExamList = new List<SelectListItem>();
            HeaderNameList = new List<string>();
            ValueList = new List<List<string>>();
            SubjectList = new List<SelectListItem>();
        }

        [Display(Name = "Session")]
        public string SessionId { get; set; }

        [Display(Name = "Term")]
        public string ExamId { get; set; }

        [Display(Name = "Class")]
        public string ClassId { get; set; }

        [Display(Name = "Section")]
        public string SectionId { get; set; }

        [Display(Name = "Subject")]
        public string SubjectId { get; set; }

        [Display(Name = "Student")]
        public string ErpNo_Id { get; set; }

        public string UserId { get; set; }

        public string Mode { get; set; }

        public string Category { get; set; }

        public int Strength { get; set; }


        public List<SelectListItem> SessionList { get; set; }
        public List<SelectListItem> ExamList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }

        public List<string> HeaderNameList { get; set; }
        public List<List<string>> ValueList { get; set; }

        public string TermName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string SubjectName { get; set; }
        public string StudentName { get; set; }
    }
}
