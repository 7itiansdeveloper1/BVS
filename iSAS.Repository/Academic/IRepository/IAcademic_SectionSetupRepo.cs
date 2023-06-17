using ISas.Entities.Academic;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_SectionSetupRepo
    {
        List<SelectListItem> GetTeacherList();

        List<Academic_SectionSetupModels> GetSectionSetupDetailsList(int Class_Strength, string Class_Name, string Class_ClassId, string SectionId);
        Academic_SectionSetupModels GetSectionSetupDetailsById(int Class_Strength, string Class_Name, string Class_ClassId, string SectionId);

        Tuple<int, string> Academic_SectionSetup_CRUD(Academic_SectionSetupModels model);
        Tuple<int, string> Academic_SectionSetup_CRUD(string ClassId, string SectionId);
    }
}
