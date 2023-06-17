using System.ComponentModel.DataAnnotations;


namespace ISas.Entities.Academic
{
    public class Academic_WingSetupModels
    {
        public string WingId { get; set; }

        [StringLength(50, ErrorMessage ="Max 50 character is allowed..")]
        [Required(ErrorMessage ="Wing Name is Req..")]
        [Display(Name ="Wing Name")]
        public string WingName { get; set; }

        [Display(Name = "Default")]
        public bool Default { get; set; }

        [Display(Name = "Print Order")]
        public int PrintOrder { get; set; }
        public int SchoolId { get; set; }
        public bool IsDeleteable { get; set; }

        //Extra Feild
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
