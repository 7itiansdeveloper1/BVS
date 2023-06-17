using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.HR_Payroll_Entities
{
    public class HR_Payroll_SalaryRegisterModels
    {
        public HR_Payroll_SalaryRegisterModels()
        {
            PayBandList = new List<SelectListItem>();
            MonthList = new List<SelectListItem>();
            PayBandEmpList = new List<HR_Payroll_PayBandEmpModels>();
            SalaryRegisterList = new List<HR_Payroll_MonthlyAttenDetailModels>();
            HeadNameList = new List<string>();
        }

        [Required]
        [Display(Name ="Pay Band")]
        public string PayBandID { get; set; }

        [Required]
        [Display(Name = "Salary Month")]
        public string AttenMonthID { get; set; }

        public List<string> HeadNameList { get; set; }
        public List<SelectListItem> PayBandList { get; set; }
        public List<SelectListItem> MonthList { get; set; }
        public List<HR_Payroll_PayBandEmpModels> PayBandEmpList { get; set; }
        public List<HR_Payroll_MonthlyAttenDetailModels> SalaryRegisterList { get; set; }

        public string UserId { get; set; }
    }

    public class HR_Payroll_PayBandEmpModels
    {
        public bool Selected { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }

        public bool IsSalaryCalculate { get; set; }
    }
}
