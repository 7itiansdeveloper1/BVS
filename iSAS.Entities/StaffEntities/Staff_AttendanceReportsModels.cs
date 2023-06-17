using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.StaffEntities
{
    public class Staff_AttendanceReportsModels
    {
        public Staff_AttendanceReportsModels()
        {
            StaffList = new List<SelectListItem>();
            HeaderNameList = new List<string>();
            ValueList = new List<List<string>>();
            DepartmentList = new List<SelectListItem>();
            ReportNameList = new List<SelectListItem>();
        }

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public string Print { get; set; }
        public string ReportType { get; set; }
        public string OrderBy { get; set; }

        [Display(Name = "Report Name")]
        public string ReportName { get; set; }
        public string DepartmentId { get; set; }
        public string ReportFilterBy { get; set; }
        public bool Status { get; set; }

        public string SelectedDepartmentNames { get; set; }
        public string SelectedReportName { get; set; }
        public string SelectedStaffIds { get; set; }

        public List<SelectListItem> StaffList { get; set; }   
        public List<string> HeaderNameList { get; set; }
        public List<List<string>> ValueList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> ReportNameList { get; set; }
    }
}
