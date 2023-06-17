using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.TransportEntities
{
    public class Transport_VehicleSetupModel
    {
        public Transport_VehicleSetupModel()
        {
            VehicleTypeList = new List<SelectListItem>();
            BloodGrpList = new List<SelectListItem>();
        }

        //Vehicle Details
        public string VehID { get; set; }

        [StringLength(50, ErrorMessage ="Maximum 50 Char. is Allowed..!")]
        [Required(ErrorMessage ="Vehicle Name is Req..!")]
        [Display(Name ="Name")]
        public string VehName { get; set; }

        [Display(Name = "Reg. No")]
        public string VehNo { get; set; }

        [Display(Name = "Type")]
        public string VehType { get; set; }

        [Display(Name = "Seat Capacity")]
        public int SCapacity { get; set; }

        //Driver Details
        [Display(Name = "Name")]
        public string DName { get; set; }

        [Display(Name = "Mobile")]
        public string DMob { get; set; }

        [Display(Name = "Blood Group")]
        public string DBldGrp { get; set; }

        [Display(Name = "DL-No")]
        public string DDLNo { get; set; }

        [Display(Name = "Bedge No")]
        public string DBedgeNo { get; set; }

        [Display(Name = "Address")]
        public string DAddress { get; set; }

        //Helper Details
        [Display(Name ="Name")]
        public string HName { get; set; }

        [Display(Name = "Mobile")]
        public string HMob { get; set; }

        [Display(Name = "Blood Group")]
        public string HBldGrp { get; set; }

        [Display(Name = "DL-No")]
        public string HDLNo { get; set; }

        [Display(Name = "Bedge No")]
        public string HBedgeNo { get; set; }

        [Display(Name = "Address")]
        public string HAddress { get; set; }

        //Incharge Details
        [Display(Name = "Incharge Name")]
        public string InchargeName { get; set; }

        [Display(Name = "Incharge Mobile")]
        public string InchargeMobile { get; set; }

        [Display(Name = "Parent 1 Name")]
        public string Parent1Name { get; set; }

        [Display(Name = "Parent 1 Mobile")]
        public string Parent1Mobile { get; set; }

        [Display(Name = "Parent 2 Name")]
        public string Parent2Name { get; set; }

        [Display(Name = "Parent 2 Mobile")]
        public string Parent2Mobile { get; set; }


        //public string CBy { get; set; }
        //public string CDate { get; set; }
        //public string MBy { get; set; }
        //public string MDate { get; set; }
        public int Strength { get; set; }
        public bool IsDeleteable { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }

        public List<SelectListItem> VehicleTypeList { get; set; }
        public List<SelectListItem> BloodGrpList { get; set; }
    }

    public class VehicleRouteSetupModels
    {
        public VehicleRouteSetupModels()
        {
            VehicleRouteDetails = new List<SelectListItem>();
        }

        public string VehicleId { get; set; }
        public string RouteIds { get; set; }
        public string UserId { get; set; }

        public List<SelectListItem> VehicleRouteDetails { get; set; }
    }
}
