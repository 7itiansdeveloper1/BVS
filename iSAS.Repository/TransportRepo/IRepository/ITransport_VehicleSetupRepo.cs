using ISas.Entities.TransportEntities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.TransportRepo.IRepository
{
    public interface ITransport_VehicleSetupRepo
    {
        List<SelectListItem> GetBloodGroupList();
        List<Transport_VehicleSetupModel> GetVehicleList(string VehicleId);
        Transport_VehicleSetupModel GetVehicleById(string VehicleId);

        Tuple<int, string> Transport_VehicleSetup_CRUD(Transport_VehicleSetupModel model);
        Tuple<int, string> Transport_VehicleSetup_CRUD(string VehicleId);

        VehicleRouteSetupModels GetVehicleRouteDetails(string VehicleId);
        Tuple<int, string> VehicleRouteSetup_CRUD(VehicleRouteSetupModels model);
    }
}
