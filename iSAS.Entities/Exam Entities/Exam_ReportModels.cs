using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ISas.Entities.CommonEntities;

namespace ISas.Entities.Exam_Entities
{

    public class Exam_ReportModels
    {
        public Exam_ReportModels()
        {
            ClassSectionList = new List<SelectListItem>();
            ValueList = new List<List<string>>();
            HeaderNameList = new List<string>();
            SubjectList = new List<SelectListItem>();
            ReportNameList = new List<SelectListItem>();
            ReportHeader = new ReportHeaderEntities();
        }
        [Display(Name = "Report Name")]
        public string ReportName { get; set; }
        public string ReportId { get; set; }
        public string ClassSectionIds { get; set; }
        public string SubjectId { get; set; }
        public string SelectedClassNames { get; set; }
        public string SelectedReportName { get; set; }

        //public List<ClassSectionModel> ClassSectionList { get; set; }
        public List<SelectListItem> ClassSectionList { get; set; }
        public List<string> HeaderNameList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
        public List<SelectListItem> ReportNameList { get; set; }
        public ReportHeaderEntities ReportHeader { get; set; }
        public List<List<string>> ValueList { get; set; }
    }
}

