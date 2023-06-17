using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities
{
    public class Exam_StudentProfileModels
    {
        public Exam_StudentProfileModels()
        {
            SessionList = new List<SelectListItem>();
            ExamList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            SectionList = new List<SelectListItem>();
            StudentDetailList = new List<StudentDetailModel>();
        }

        [Display(Name ="Session")]
        public string SessionId { get; set; }

        [Display(Name = "Exam")]
        public string ExamId { get; set; }

        [Display(Name = "Class")]
        public string ClassId { get; set; }

        [Display(Name = "Section")]
        public string SectionId { get; set; }

        public List<SelectListItem> SessionList { get; set; }
        public List<SelectListItem> ExamList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> SectionList { get; set; }
        public List<StudentDetailModel> StudentDetailList { get; set; }
    }

    public class StudentDetailModel
    {
        public string ERPNo { get; set; }
        public long Sno { get; set; }
        public string AdmNo { get; set; }
        public int RollNo { get; set; }
        public string StudentName { get; set; }
        public int OpenDay { get; set; }
        public decimal Attendance { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }

    }
}
