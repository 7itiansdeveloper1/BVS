using ISas.Entities.Academic;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_SchoolSetupRepo
    {
        List<Academic_SchoolSetupModels> GetSchoolList(int SchoolId);
        Academic_SchoolSetupModels GetSchoolDetailsById(int SchoolId);

        Tuple<int, string> Academic_SchoolSetup_CRUD(Academic_SchoolSetupModels models);
        Tuple<int, string> Academic_SchoolSetup_CRUD(int SchoolId);
    }
}
