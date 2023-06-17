using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.HR_Payroll_Entities
{
    public class HR_Payroll_LeaveRegisterModels
    {
        public HR_Payroll_LeaveRegisterModels()
        {
            LeaveCodeList = new List<SelectListItem>();
            StaffList = new List<SelectListItem>();
            LeaveList = new List<LeaveDetailsModels>();
            LocalLeaveList = new List<LocalLeaveModels>();
        }

        public string StaffCode { get; set; }
        public string StaffName { get; set; }

        [Display(Name ="Leave Code")]
        public string LeaveCode { get; set; }

        [Display(Name = "From")]
        public string FromDate { get; set; }

        [Display(Name = "To")]
        public string ToDate { get; set; }

        public string LeaveType { get; set; }

        [Display(Name = "Days")]
        public int BalanceDays { get; set; }
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
        public string SelectedEmpID { get; set; }

        public List<SelectListItem> LeaveCodeList { get; set; }
        public List<SelectListItem> StaffList { get; set; }
        public List<LeaveDetailsModels> LeaveList { get; set; }
        public List<LocalLeaveModels> LocalLeaveList { get; set; }

    }

    public class LeaveDetailsModels
    {
        public int TransID { get; set; }
        public string EmpID { get; set; }
        public string TransType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string LvID { get; set; }
        public string LvCode { get; set; }
        public Int16 days { get; set; }
    }

    public class LocalLeaveModels
    {
        public string LvID { get; set; }
        public string LvCode { get; set; }
        public int OpenBal { get; set; }
        public string OpenDate { get; set; }
    }
}
