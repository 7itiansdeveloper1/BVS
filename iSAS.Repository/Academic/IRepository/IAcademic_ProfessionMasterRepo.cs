using ISas.Entities.Academic;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_ProfessionMasterRepo
    {
        List<Academic_ProfessionMasterModels> GetProfessionList();
        Tuple<int, string> Academic_ProfessionMaster_CRUD(Academic_ProfessionMasterModels model);
        Tuple<int, string> Academic_ProfessionMaster_CRUD(string ProfId);
    }
}
