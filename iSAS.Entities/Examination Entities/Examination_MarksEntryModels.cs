using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Examination_Entities
{
    public class Examination_MarksEntryModels
    {
        public Examination_MarksEntryModels()
        {
            SessionList = new List<SelectListItem>();
            ExamList = new List<SelectListItem>();
            ClassList = new List<SelectListItem>();
            StudentsMarkList = new List<Examination_StudendtMarkDetails>();
            GradeList = new List<SelectListItem>();
        }

        public string SessionId { get; set; }
        public string ExamId { get; set; }
        public string ClassId { get; set; }
        public string SectionId { get; set; }
        public string SubjectId { get; set; }
        public string AssessmentId { get; set; }

        public int MaxMark { get; set; }
        public int PassingMark { get; set; }
        public int MaxMarkWeightage { get; set; }
        public bool IsMarkBased { get; set; }

        public string UserId { get; set; }


        public List<Examination_StudendtMarkDetails> StudentsMarkList { get; set; }
        public List<SelectListItem> SessionList { get; set; }
        public List<SelectListItem> ExamList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> GradeList { get; set; }
    }

    public class Examination_StudendtMarkDetails
    {
        public long sno { get; set; }
        public string ERPNo { get; set; }
        public string AdmNo { get; set; }
        public int RollNo { get; set; }
        public string Student { get; set; }
        public string Father { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsML { get; set; }
        public bool IsExempt { get; set; }
        public string MarkObtained { get; set; }
        public string Grade { get; set; }
    }
}
