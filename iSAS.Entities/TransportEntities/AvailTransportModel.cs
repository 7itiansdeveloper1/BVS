using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.TransportEntities
{
    public class AvailTransportModel
    {
        public AvailTransportModel()
        {
            RouteList = new List<SelectListItem>();
            StopList = new List<SelectListItem>();
            VehicleList = new List<SelectListItem>();
            FacilityList = new List<SelectListItem>();
            DueList = new List<TransportDuesDetails>();
        }

        public bool IsAvailTransport { get; set; }

        [Display(Name = "Route")]
        public string RouteId { get; set; }

        [Display(Name = "Stop")]
        public string StopId { get; set; }

        [Display(Name = "Vehicle")]
        public string VehicleId { get; set; }

        [Display(Name = "Facility")]
        public string FacilityId { get; set; }

        [Display(Name = "Driver Name")]
        public string DriverName { get; set; }

        [Display(Name = "Driver Mobile")]
        public string DriverMobile { get; set; }

        [Display(Name = "Helper Name")]
        public string HelperName { get; set; }

        [Display(Name = "Seat Capacity")]
        public int SeatCapacity { get; set; }

        [Display(Name = "Seat Occupied")]
        public int SeatOccupied { get; set; }

        [Display(Name = "Helper No")]
        public string HelperNo { get; set; }

        [Display(Name = "Incharge")]
        public string Incharge { get; set; }

        [Display(Name = "Incharge Mobile")]
        public string InchargeMobile { get; set; }

        [Display(Name = "Pick-Up Charge")]
        public int PickCharge { get; set; }

        [Display(Name = "Drop Charge")]
        public int DropCharge { get; set; }

        [Display(Name = "Pick & Drop Charge")]
        public int PickAndDropCharge { get; set; }

        [Display(Name ="w.e.f. Date")]
        public string Date { get; set; }

        public List<SelectListItem> RouteList { get; set; }
        public List<SelectListItem> StopList { get; set; }
        public List<SelectListItem> VehicleList { get; set; }
        public List<SelectListItem> FacilityList { get; set; }

        public List<TransportDuesDetails> DueList { get; set; }
        public  StudentSearchModel StudentDetails { get; set; }

        //Extra Feild
        public string CRUDFor { get; set; }
        public string Selected_FeeHeadId { get; set; }
        public string Selected_DueDate { get; set; }
        public int Selected_TransportAmount { get; set; }
        public string Selected_TransRefNo { get; set; }
        public bool IsSingleSave { get; set; }
        public string UserId { get; set; }
    }

    public class TransportDuesDetails
    {
        public string DueDate { get; set; }
        public string HeadID { get; set; }
        public string HeadName { get; set; }
        public int Due { get; set; }
        public int Paid { get; set; }
        public int Balance { get; set; }
        public int Excess { get; set; }
        public bool IsEditable { get; set; }

        public bool WithDrawlTransport { get; set; }
        public bool AvailTransport { get; set; }
        public string TransRefNo { get; set; }
    }
}
