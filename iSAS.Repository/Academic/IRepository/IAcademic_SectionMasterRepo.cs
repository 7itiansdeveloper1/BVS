using ISas.Entities.Academic;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.Academic.IRepository
{
    public  interface IAcademic_SectionMasterRepo
    {
        List<Academic_SectionMasterModels> GetAcademic_SectionMasterList(string SecId);
        Academic_SectionMasterModels GetAcademic_SectionMasterById(string SecId);

        Tuple<int, string> Academic_SectionMaster_CRUD(Academic_SectionMasterModels model);
        Tuple<int, string> Academic_SectionMaster_CRUD(string SecId);
        List<SelectListItem> getAllSectionByClass(string classId);
        List<SelectListItem> getSectionByClass_USer(string classId, string userId);
    }
}
