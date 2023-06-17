using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.FeesEntities
{
    public class Fee_FeeStructureMasterModels
    {
        public string StructId { get; set; }

        [Required(ErrorMessage ="Structure Name is Req..!")]
        [Display(Name = "Structure Name")]
        public string StructName { get; set; }

        [Display(Name = "Is Default")]
        public bool Default { get; set; }

        [Display(Name = "Print Order")]
        public int PrintOrder { get; set; }

        public string Installment { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDeletetable { get; set; }
        public bool InstallmentSetup { get; set; }
        public bool DueSetup { get; set; }
        public bool FineSetup { get; set; }

        //Extra Feild
        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } //Not Getting from get
        public string CRUDMode { get; set; }
        public string UserId { get; set; }

    }
}
