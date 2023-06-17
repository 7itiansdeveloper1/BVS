using ISas.Entities.Academic;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_WingSetupRepo
    {
        List<Academic_WingSetupModels> GetWingList(string SchoolName, int SchoolId, string WingId);
        Academic_WingSetupModels GetWingDetailsById(string SchoolName, int SchoolId, string WingId);

        Tuple<int, string> Academic_WingSetup_CRUD(Academic_WingSetupModels model);
        Tuple<int, string> Academic_WingSetup_CRUD(string WingId);
    }
}
