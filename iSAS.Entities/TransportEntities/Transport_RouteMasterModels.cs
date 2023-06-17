

using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.TransportEntities
{
    public class Transport_RouteMasterModels
    {
        public string RouteId { get; set; }

        [StringLength(50, ErrorMessage = "Max 50 char. is allowed..!")]
        [Required(ErrorMessage = "Route Name is Req..!")]
        [Display(Name = "Route Name")]
        public string RouteName { get; set; }


        // [Required(ErrorMessage ="Print Order is Req..!")]
        [Display(Name = "Print Order")]
        public int PrintOrder { get; set; }


        public bool Active { get; set; }
        public int NoofVehicle { get; set; }
        public int NoofStop { get; set; }
        public int Strength { get; set; }
        public bool IsDeleteable { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
