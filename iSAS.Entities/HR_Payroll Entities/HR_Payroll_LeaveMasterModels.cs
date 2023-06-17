using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.HR_Payroll_Entities
{
    public class HR_Payroll_LeaveMasterModels
    {
        //10
        public string LvID { get; set; }

        [Required(ErrorMessage = "Leave Code is Req..!")]
        [StringLength(20, ErrorMessage ="Max 20 Char. is allowed..!")]
        [Display(Name ="Leave Code")]
        public string LvCode { get; set; }

        [Required(ErrorMessage ="Leave Type is Req..!")]
        [StringLength(20, ErrorMessage = "Max 20 Char. is allowed..!")]
        [Display(Name = "Leave Type")]
        public string LeaveType { get; set; }

        [Display(Name = "Carrry Forward?(Y/N)")]
        public bool CF { get; set; }

        [Display(Name = "Annual Quota of Leave")]
        public Int16 AnnQuota { get; set; }

        [Display(Name = "Max Allumination of Leave Allowed")]
        public Int16 MaxAllow { get; set; }

        [Display(Name = "Paid(Y/N)")]
        public bool Paid { get; set; }

        [Display(Name = "Balance(Y/N)")]
        public bool Balance { get; set; }

        [Display(Name = "Running/Working")]
        public bool Running { get; set; }

        [Display(Name = "Encash.(Y/N)")]
        public bool Encash { get; set; }
        public bool IsDeleteable { get; set; }
        public bool IsEditable { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
