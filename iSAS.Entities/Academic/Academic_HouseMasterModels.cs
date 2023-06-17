using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.Academic
{
    public class Academic_HouseMasterModels
    {
        public string HouseId { get; set; }

        [StringLength(50, ErrorMessage = "Max. 50 Char is Allowed..!")]
        [Required(ErrorMessage ="House Name is Req..!")]
        [Display(Name ="House Name")]
        public string HouseName { get; set; }

        [Display(Name = "House Color")]
        public string HouseColor { get; set; }

        [Display(Name = "Incharge Name")]
        public string HouseInchargeName { get; set; }

        [Display(Name ="Print Order")]
        public int PrintOrder { get; set; }


        public bool Active { get; set; }
        public int HouseStrength { get; set; }
        public bool IsDeletable { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
