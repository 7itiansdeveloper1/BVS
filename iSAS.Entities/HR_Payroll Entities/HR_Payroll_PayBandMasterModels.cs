using System;
using System.ComponentModel.DataAnnotations;


namespace ISas.Entities.HR_Payroll_Entities
{
    public class HR_Payroll_PayBandMasterModels
    {
        public string PayBandId { get; set; }

        [Display(Name = "PayBand Name")]
        [StringLength(20)]
        public string PayBandName { get; set; }

        [StringLength(50)]
        public string Discrption { get; set; }

        [Display(Name = "Print Order")]
        public Int16 PrintOrder { get; set; }
        public bool Active { get; set; }
        public bool IsDeletable { get; set; }


        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
