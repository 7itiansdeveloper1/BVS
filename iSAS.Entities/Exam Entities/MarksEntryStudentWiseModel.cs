using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities
{
    public class MarksEntryStudentWiseModel
    {

        public MarksEntryStudentWiseModel()
        {
            SubjectListWithMarks = new List<SubjectModel>();
            AssismentNameList = new List<AssismentModel>();
            SessionList = new List<SelectListItem>();
            ExamList = new List<SelectListItem>();
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


        public List<SubjectModel> SubjectListWithMarks { get; set; }
        public List<AssismentModel> AssismentNameList { get; set; }
        public List<SelectListItem> SessionList { get; set; }
        public List<SelectListItem> ExamList { get; set; }

        //Extra Feild
        public string UserID { get; set; }

        public string TermName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string MainSubjectName { get; set; }
        public string StudentName { get; set; }
    }

    public class SubjectModel
    {
        public SubjectModel()
        {
            AssismentWithMarksList = new List<AssismentModel>();
        }

        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }

        public List<AssismentModel> AssismentWithMarksList { get; set; }
    }

    public class AssismentModel
    {
        public string AssismentName { get; set; }
        public string AssismentCode { get; set; }
        public string Marks { get; set; }
    }
}
