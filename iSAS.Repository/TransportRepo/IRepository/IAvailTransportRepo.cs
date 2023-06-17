using ISas.Entities.TransportEntities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.TransportRepo.IRepository
{
    public interface IAvailTransportRepo
    {
        List<SelectListItem> Get_AvailTransportDropDowns(string RouteId, string QueryFor, string StopId, string ERPNo, string SessionId);
        List<TransportDuesDetails> Get_TransportDueList(string RouteId, string QueryFor, string StopId, string ERPNo, string SessionId);

        AvailTransportModel Get_TransportDetails(string RouteId, string QueryFor, string StopId, string ERPNo, string SessionId);

        Tuple<int, string> AvailTransport_CRUD(AvailTransportModel model);
        Tuple<int, string> Transportation_CRUD(AvailTransportModel model);

        Tuple<int, int, string, string, string, string, string> Get_VehicleDetails(string VehicleId);
        Tuple<int, int, int> Get_ChargeDetails(string StopId);
    }
}
