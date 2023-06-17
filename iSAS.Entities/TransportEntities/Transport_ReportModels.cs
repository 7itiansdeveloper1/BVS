using ISas.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.TransportEntities
{
    public class Transport_RouteDetailsModel
    {
        public string VehId { get; set; }
        public int Sno { get; set; }
        public string Vehicle { get; set; }
        public string RouteName { get; set; }
        public string DriverName { get; set; }
        public int TotalSeat { get; set; }
        public int Availability { get; set; }
    }

    public class Transport_BusStudentDetails
    {
        public int Sno { get; set; }
        public string AdmNo { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string StopName { get; set; }
        public string Facility { get; set; }
        public int Charge { get; set; }
    }

    public class Transport_ReportModel
    {
        public Transport_ReportModel()
        {
            ReportNameList = new List<SelectListItem>();
            FeeCategoryList = new List<SelectListItem>();
            InstallmentList = new List<SelectListItem>();
            RouteList = new List<SelectListItem>();
            VehicleList = new List<SelectListItem>();
            FeeStructureList = new List<SelectListItem>();
            ReportHeaders = new ReportHeaderEntities();
            OtherReportHeader = new Transport_OtherReportHeader();
            HeaderNameList = new List<string>();
            ValueList = new List<List<string>>();
        }

        [Display(Name = "Report Name")]
        public string ReportId { get; set; }

        [Display(Name = "Fee Category")]
        public string FeeCategoryId { get; set; }

        [Display(Name = "Installment")]
        public string InstallmentId { get; set; }

        [Display(Name = "Route")]
        public string RouteId { get; set; }

        [Display(Name = "Vehicle")]
        public string VehicleId { get; set; }

        public List<string> HeaderNameList { get; set; }
        public List<List<string>> ValueList { get; set; }

        public string SessionId { get; set; }
        public string TotalAmount { get; set; }

        public ReportHeaderEntities ReportHeaders { get; set; }

        public Transport_OtherReportHeader OtherReportHeader { get; set; }

        public List<SelectListItem> ReportNameList { get; set; }
        public List<SelectListItem> FeeCategoryList { get; set; }
        public List<SelectListItem> InstallmentList { get; set; }
        public List<SelectListItem> RouteList { get; set; }
        public List<SelectListItem> VehicleList { get; set; }
        public List<SelectListItem> FeeStructureList { get; set; }
    }

    public class Transport_OtherReportHeader
    {
        public string RouteName { get; set; }
        public string VehName { get; set; }
        public string DName { get; set; }
        public string DMob { get; set; }
        public string DDLNo { get; set; }
        public string DBedgeNo { get; set; }
        public string HName { get; set; }
        public string HMob { get; set; }
        public string InchargeName { get; set; }
        public string InchargeMobile { get; set; }
        public string Parent1Name { get; set; }
        public string Parent1Mobile { get; set; }
        public string Parent2Name { get; set; }
        public string Parent2Mobile { get; set; }
    }
}
