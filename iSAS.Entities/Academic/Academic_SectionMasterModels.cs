using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.Academic
{
    public class Academic_SectionMasterModels
    {
        public string SecId { get; set; }

        [StringLength(20, ErrorMessage ="Only 20 Char. is Allowed.!")]
        [Required(ErrorMessage = "Section Name is Req..!")]
        [Display(Name = "Section Name")]
        public string SecName { get; set; }

        [Display(Name = "Print Order")]
        public int PrintOrder { get; set; }

        [Display(Name = "Is Active")]
        public bool Active { get; set; }


        public bool IsDeletable { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
