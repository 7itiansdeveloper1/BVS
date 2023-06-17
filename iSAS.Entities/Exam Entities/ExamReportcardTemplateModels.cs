using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Exam_Entities
{
    public class ExamReportcardTemplateModels
    {
        public ExamReportcardTemplateModels()

        {
            examList = new List<SelectListItem>();
            reportcardTemplateList = new List<ReportcardTemplateList>();
        }
        public List<SelectListItem> examList { get; set; }
        public List<ReportcardTemplateList> reportcardTemplateList { get; set; }
        [Display(Name = "Exam")]
        public string examId { get; set; }

    }

    public class ReportcardTemplateList
    {
        public string examId { get; set; }
        public string classId { get; set; }
        public string className { get; set; }
        public string termStartDate { get; set; }
        public string termEndDate { get; set; }
        public string reportCardDate { get; set; }
        public bool haveOrientation { get; set; }
        public string termFirstPTMDate { get; set; }
        public string termLastPTMDate { get; set; }
        public bool isTermLockforClass { get; set; }


    }

}
