using System.ComponentModel.DataAnnotations;


namespace ISas.Entities.FeesEntities
{
    public class Fee_ReceiptHeaderMasterModels
    {
        [StringLength(10)]
        public string HeaderId { get; set; }

        [Display(Name = "Header 1")]
        [Required(ErrorMessage ="Header 1 is Req..!")]
        [StringLength(200)]
        public string Header1 { get; set; }

        [Display(Name = "Header 2")]
        [StringLength(200)]
        public string Header2 { get; set; }

        [Display(Name = "Header 3")]
        [StringLength(200)]
        public string Header3 { get; set; }

        [Display(Name = "Header 4")]
        [StringLength(200)]
        public string Header4 { get; set; }

        [Display(Name = "Logo")]
        [StringLength(200)]
        public string Logo { get; set; }


        [Display(Name = "Use Prefix?")]
        public bool UsePrefix { get; set; }

        [Display(Name = "Prefix")]
        [StringLength(10)]
        public string Prefix { get; set; }

        [Display(Name = "First Receipt No.")]
        public int ReceiptStartNo { get; set; }

        [Display(Name = "Header For")]
        [Required(ErrorMessage ="Header For is Req..!")]
        [StringLength(10)]
        public string HeaderFor { get; set; }

        public string UserId { get; set; }
        public string CRUDMode { get; set; }

        [Display(Name = "Is Enable?")]
        public string IsEnableStr { get; set; }

        [Display(Name ="Is Enable")]
        public bool IsEnable { get; set; }
    }
    public class Fee_WingHeaderSetupModel
    {
        public string WingID { get; set; }
        public string WingName { get; set; }
        public string Header1 { get; set; }
        public string Header2 { get; set; }
        public string Header3 { get; set; }
        public string Header4 { get; set; }
        public string Logo { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDeleteable { get; set; }

        public string CRUDMode { get; set; }
    }
}
