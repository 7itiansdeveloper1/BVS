using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.Exam_Entities
{
    public class Exam_QuestionPaperModels
    {
        public Exam_QuestionPaperModels()
        {
            qPaperList = new List<Exam_QuestionPaperList>();
            availablePaperList = new List<Exam_AvailablePaperList>();
        }


        public List<Exam_QuestionPaperList> qPaperList { get; set; }
        public List<Exam_AvailablePaperList> availablePaperList { get; set; }
    }

    public class Exam_QuestionPaperList
    {
        public int qpId { get; set; }
        public string   classId { get; set; }
        public string ClassName { get; set; }
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string assessmentId { get; set; }
        public string AssessmentName { get; set; }
        public string docPath { get; set; }
        public string createdBy { get; set; }
        public int MaxMark { get; set; }
        public bool isActive { get; set; }

    }

    public class Exam_AvailablePaperList
    {
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string AssessmentId { get; set; }
        public string AssessmentName { get; set; }
        public string subjectId { get; set; }
        public string SubjectName { get; set; }
        public int MM { get; set; }
        public int  ClassPrintOrder { get; set; }
        public int ExamPrintOrder { get; set; }
        public bool isQPCreated { get; set; }
        

    }

    public class Exam_AnswersheetModels
    {

        public Exam_AnswersheetModels()
        {
            answersheetLists = new List<AnswersheetList>();
        }
        public string ClassName { get; set; }
        public string AssessmentName { get; set; }
        public string SubjectName { get; set; }
        public List<AnswersheetList> answersheetLists = new List<AnswersheetList>();
    }

    public class AnswersheetList
    {
        public string ERPNO { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string ansDocPath { get; set; }
    }


}
