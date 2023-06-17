using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.FeesEntities
{
    public class Fee_InstallmentSetupModels
    {
        public string InstallId { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 character is allowed..!")]
        [Required(ErrorMessage ="Installment Name is Req..!")]
        [Display(Name ="Installment Name")]
        public string InstallName { get; set; }

        [StringLength(20, ErrorMessage ="Max 20 character is allowed..!")]
        [Display(Name = "Installment Alias")]
        public string InstallAlias { get; set; }

        [Display(Name = "Due Date")]
        public string InstallDueDate { get; set; }

        [Display(Name = "First Date")]
        public string InstallFirstDate { get; set; }

        [Display(Name = "Last Date")]
        public string InstallLastDate { get; set; }

        [Display(Name = "Print Order")]
        public int PrintOrder { get; set; }

        [Display(Name = "Is Active")]
        public bool Active { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDeletetable { get; set; }

        public string FineStartDate { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
        public string StructId { get; set; }
        public string StrectureName { get; set; }
        public string SessionId { get; set; }
    }
}
