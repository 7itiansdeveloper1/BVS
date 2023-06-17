using ISas.Entities.Academic;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_HouseMasterRepo
    {
        List<Academic_HouseMasterModels> GetHouseList();

        Tuple<int, string> Academic_HouseMaster_CRUD(Academic_HouseMasterModels model);
        Tuple<int, string> Academic_HouseMaster_CRUD(string HouseId);
    }
}
