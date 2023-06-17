using ISas.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.LibraryEntities
{
    
    public class Library_ReportModel
    {
        public Library_ReportModel()
        {
            ValueList = new List<List<string>>();
            ReportNameList = new List<SelectListItem>();
            HeaderNameList = new List<string>();
            ValueList = new List<List<string>>();
            ReportHeader = new ReportHeaderEntities();
        }

        [Display(Name = "Report Name")]
        public string reportId { get; set; }
        [Display(Name = "From")]
        public string fromDate { get; set; }
        [Display(Name = "To")]
        public string toDate { get; set; }
        public List<SelectListItem> ReportNameList { get; set; }
        public ReportHeaderEntities ReportHeader { get; set; }
        public List<string> HeaderNameList { get; set; }
        public List<List<string>> ValueList { get; set; }

    }
}
