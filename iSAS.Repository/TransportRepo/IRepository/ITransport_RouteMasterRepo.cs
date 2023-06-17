using ISas.Entities.TransportEntities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.TransportRepo.IRepository
{
    public interface ITransport_RouteMasterRepo
    {
        List<Transport_RouteMasterModels> GetRouteMasterList(string RouteId);
        Transport_RouteMasterModels GetRouteMasterById(string RouteId);

        Tuple<int, string> Transport_RouteMaster_CRUD(Transport_RouteMasterModels model);
        Tuple<int, string> Transport_RouteMaster_CRUD(string RouteId);

        List<SelectListItem> getRouteMstDropDownList();
    }
}
