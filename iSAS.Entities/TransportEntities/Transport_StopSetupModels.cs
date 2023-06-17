using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.TransportEntities
{
    public class Transport_StopSetupModels
    {
        public string StopID { get; set; }

        [StringLength(50, ErrorMessage ="Maximum 50 Char is Allowed..!")]
        [Required(ErrorMessage ="Stop Name is Req..!")]
        [Display(Name ="Stop Name")]
        public string StopName { get; set; }

        [Display(Name = "Pick & Drop Charges")]
        public int PDCharge { get; set; }

        [Display(Name = "Pick Charge")]
        public int PCharge { get; set; }

        [Display(Name = "Drop Charge")]
        public int DCharge { get; set; }

        [Display(Name = "Pick-Up Time")]
        public string PickupTime { get; set; }

        [Display(Name = "Drop Time")]
        public string DropTime { get; set; }

        [Display(Name = "Print Order")]
        public int PrintOrder { get; set; }


        public bool Active { get; set; }
        public string RouteId { get; set; }

        [Display(Name = "Route Name")]
        public string RouteName { get; set; }

        [Display(Name = "Strength")]
        public int Strength { get; set; }
        public bool IsDeleteable { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
