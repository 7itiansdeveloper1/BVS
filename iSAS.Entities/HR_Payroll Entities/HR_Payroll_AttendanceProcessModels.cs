using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.HR_Payroll_Entities
{
    public class HR_Payroll_AttendanceProcessModels
    {
        public HR_Payroll_AttendanceProcessModels()
        {
            PayBandList = new List<SelectListItem>();
            MonthList = new List<SelectListItem>();
            MontyelyAttenList = new List<HR_Payroll_MonthlyAttenDetailModels>();
            HeadNameList = new List<string>();
        }

        [Display(Name ="Pay Band")]
        public string PayBandID { get; set; }

        [Display(Name = "Attendance Month")]
        public string AttenMonthID { get; set; }

        public List<string> HeadNameList { get; set; }

        public List<SelectListItem> PayBandList { get; set; }
        public List<SelectListItem> MonthList { get; set; }

        public List<HR_Payroll_MonthlyAttenDetailModels> MontyelyAttenList { get; set; }
    }

    public class HR_Payroll_MonthlyAttenDetailModels
    {
        public HR_Payroll_MonthlyAttenDetailModels()
        {
            ValuesList = new List<string>();
        }

        public string StaffID { get; set; }
        public string StaffName { get; set; }
        public List<string> ValuesList { get; set; }
    }
}
