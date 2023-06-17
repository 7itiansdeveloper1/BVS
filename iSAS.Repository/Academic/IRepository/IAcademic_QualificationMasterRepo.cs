using ISas.Entities.Academic;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_QualificationMasterRepo
    {
        List<Academic_QualificationMasterModels> GetQualificationList();
        Tuple<int, string> Academic_QualificationMaster_CRUD(Academic_QualificationMasterModels model);
        Tuple<int, string> Academic_QualificationMaster_CRUD(string QualifId);
    }
}
