using ISas.Entities.Academic;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_ClassSetupRepo
    {
        List<Academic_ClassSetupModels> GetClassList(string WingName, string WingId, string ClassId);
        Academic_ClassSetupModels GetClassById(string WingName, string WingId, string ClassId);

        Tuple<int, string> Academic_ClassSetup_CRUD(Academic_ClassSetupModels model);
        Tuple<int, string> Academic_ClassSetup_CRUD(string ClassId);

        List<SelectListItem> All_ClassList_DropDown();
        List<SelectListItem> All_ClassWithSectionList_DropDown();
        List<SelectListItem> RegOpen_ClassList_DropDown();
        List<SelectListItem> Board_ClassSecList_DropDown(string SessionId);
        List<SelectListItem> All_ClassWithSectionList_DropDown(string userId);
    }
}
