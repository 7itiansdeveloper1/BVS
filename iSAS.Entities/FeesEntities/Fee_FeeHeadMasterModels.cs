using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.FeesEntities
{
    public class Fee_FeeHeadMasterModels
    {
        public string HeadID { get; set; }

        [Required(ErrorMessage ="Head Name is Req..!")]
        [Display(Name = "Head Name")]
        public string HeadName { get; set; }

        [Display(Name = "Head Alias")]
        public string HeadAlias { get; set; }

        [Display(Name = "New Admission")]
        public bool NewAdm { get; set; }

        [Display(Name = "Installment")]
        public bool Installment { get; set; }

        [Display(Name = "Annual")]
        public bool Annual { get; set; }

        [Display(Name = "Is Non-Refundable")]
        public bool IsNonRefundable { get; set; }

        [Display(Name = "Is Adjustable")]
        public bool IsAdjustable { get; set; }

        [Display(Name = "Is Main Head")]
        public bool IsMainHead { get; set; }

        [Display(Name = "Is Fine Enable Head")]
        public bool IsFineEnableHead { get; set; }

        [Display(Name = "Is Transport Head")]
        public bool IsTransportHead { get; set; }

        [Display(Name = "Is Advance Head")]
        public bool IsAdvanceHead { get; set; }

        [Display(Name = "Is Discount Head")]
        public bool IsDiscountHead { get; set; }

        [Display(Name = "Is Concession Head")]
        public bool IsConcessionHead { get; set; }

        [Display(Name = "Is Fine Head")]
        public bool IsFineHead { get; set; }

        [Display(Name = "Is Misc. Head")]
        public bool IsMiscHead { get; set; }

        [Display(Name = "Print Order")]
        public int PrintOrder { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDeletetable { get; set; }


        [Display(Name ="Adjustment Order")]
        public short AdjustmentOrder { get; set; }

        [Display(Name = "Is Open Balance Head")]
        public bool IsOpenBalanceHead { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
