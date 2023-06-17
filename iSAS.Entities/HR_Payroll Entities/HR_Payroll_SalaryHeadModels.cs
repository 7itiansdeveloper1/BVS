using System;
using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.HR_Payroll_Entities
{
    public class HR_Payroll_SalaryHeadModels
    {
        public string HeadID { get; set; }

        [Required]
        [Display(Name ="Salary Head")]
        public string HeadName { get; set; }

        [Display(Name = "Description")]
        public string Discrption { get; set; }

        [Display(Name = "Head Type")]
        public string HeadType { get; set; }

        [Display(Name = "Expense to School?")]
        public bool ExpansetoComp { get; set; }

        [Display(Name = "Day Wise Calculation?")]
        public bool Daywise { get; set; }


        public bool IsDeletable { get; set; }
        public Int16 PrintOrder { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
