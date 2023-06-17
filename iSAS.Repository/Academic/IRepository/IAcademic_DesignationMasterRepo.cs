using ISas.Entities.Academic;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_DesignationMasterRepo
    {
        List<Academic_DesignationMasterModels> GetDesignationList();
        Tuple<int, string> Academic_DesignationMaster_CRUD(Academic_DesignationMasterModels model);
        Tuple<int, string> Academic_DesignationMaster_CRUD(string DesigId);
    }
}
