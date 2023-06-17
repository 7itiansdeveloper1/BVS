using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.StaffEntities
{
    public class Staff_ReportsModels
    {
        public Staff_ReportsModels()
        {
            StaffDetailList = new List<StaffDetailsModel>();
            HeaderNameList = new List<string>();
            ValueList = new List<List<string>>();
            DepartmentList = new List<SelectListItem>();
            ReportNameList = new List<SelectListItem>();
            //StaffDetailReport = new List<StaffDetailReportModel>();
        }

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

        public List<StaffDetailsModel> StaffDetailList { get; set; }
        public List<string> HeaderNameList { get; set; }
        public List<List<string>> ValueList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> ReportNameList { get; set; }
        //public List<StaffDetailReportModel> StaffDetailReport { get; set; }
    }

    public class StaffDetailsModel
    {
        public bool Selected { get; set; }
        public string StaffName { get; set; }
        public string StaffId { get; set; }
        public string DeptId { get; set; }
        public string DeptName { get; set; }
    }

    //public class StaffDetailReportModel
    //{
    //    public string StaffID { get; set; }
    //    public string Staff { get; set; }
    //    public string Name { get; set; }
    //    public string DOB { get; set; }
    //    public string Category { get; set; }
    //    public string ReligonName { get; set; }
    //    public string Blood { get; set; }
    //    public string Group { get; set; }
    //    public string Martial { get; set; }
    //    public string Status { get; set; }
    //    public string Qualification { get; set; }
    //    public string Designation { get; set; }
    //    public string DOJ { get; set; }
    //    public string DOL { get; set; }
    //    public string FatOrHusName { get; set; }
    //    public string Mother { get; set; }
    //    public string Address1 { get; set; }
    //    public string Address2 { get; set; }
    //    public string Ph1 { get; set; }
    //    public string Ph2 { get; set; }
    //    public string MobileNo { get; set; }
    //    public bool Active { get; set; }
    //    public string HRNo { get; set; }

    //}
}
