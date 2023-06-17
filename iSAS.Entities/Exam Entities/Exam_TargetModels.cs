using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Exam_Entities
{
    public class Exam_TargetModels
    {
        public Exam_TargetModels()
        {
            SubjectList = new List<SelectListItem>();
        }

        public string SessionId { get; set; }
        public string ClassSectionId { get; set; }
        public string ClassName { get; set; }
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
        public List<SelectListItem> SessionList { get; set; }
        public List<SelectListItem> ClassSectionList { get; set; }
        public List<Exam_TargetList> TargetList { get; set; }
    }

    public class Exam_TargetList
    {
        public string  ERPNO { get; set; }
        public string Student { get; set; }
        public string UT { get; set; }
        public string HY { get; set; }
        public string UTBEST { get; set; }
        public string Final { get; set; }
        public string Total { get; set; }
        public string PassingMark { get; set; }
        public string Target { get; set; }

    }
    

}
