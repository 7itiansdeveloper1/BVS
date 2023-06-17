using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.Exam_Entities
{
    public class Exam_OnlineAssessmentModels
    {
        public Exam_OnlineAssessmentModels()
        {
            OnlineAssessmentList = new List<Exam_OnlineAssessmentModels>();
        }
        public int qpId { get; set; }
        public string subjectName { get; set; }
        public int maxMark { get; set; }
        public string setBy { get; set; }
        public string docPath { get; set; }
        public string ansDocPath { get; set; }
        public bool Attend { get; set; }
        public bool Result { get; set; }
        public string TAT { get; set; }
        List<Exam_OnlineAssessmentModels> OnlineAssessmentList = new List<Exam_OnlineAssessmentModels>();
    }
}
