using ISas.Entities.TransportEntities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.TransportRepo.IRepository
{
    public interface ITransport_ReportRepo
    {
        List<Transport_RouteDetailsModel> Transport_Report_RouteDetails();
        List<Transport_BusStudentDetails> Transport_Report_BusStudentDetails(string VehicleID);

        Transport_ReportModel Transport_Report_FormLoad();
        List<SelectListItem> GetRouteOrVehicleList(string RouteId, string QueryFor);
        Transport_ReportModel Transport_ReportDetails(Transport_ReportModel parmVal);
    }
}
