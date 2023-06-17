using ISas.Entities.TransportEntities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.TransportRepo.IRepository
{
    public interface ITransport_StopSetupRepo
    {
        List<Transport_StopSetupModels> GetStopList(string RouteId, string StopId);
        Transport_StopSetupModels GetStopById(string RouteId, string StopId);

        Tuple<int, string> Transport_StopSetup_CRUD(Transport_StopSetupModels model);
        Tuple<int, string> Transport_StopSetup_CRUD(string StopId);
    }
}
