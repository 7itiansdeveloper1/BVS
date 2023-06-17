using ISas.Entities.Academic;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_HolidayAllocationRepo
    {
        //List<SelectListItem> GetDepartmentList();
        List<Academic_HolidayAllocationModel> GetHolidayAllocationList();
        Academic_HolidayAllocationModel GetStaffList(Academic_HolidayAllocationModel model);

        string Academic_HolidayAllocation_CRUD(string HolidayId, string ReferenceIds, string CRUDFor);
    }
}
