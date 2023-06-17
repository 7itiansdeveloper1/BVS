using ISas.Entities.Academic;
using System.Collections.Generic;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_HolidayMasterRepo
    {
        List<Academic_HolidayMasterModel> GetHolidayMasterList();

        string Academic_HolidayMaster_CRUD(Academic_HolidayMasterModel model);
        string Academic_HolidayMaster_CRUD(string HolidayId);
    }
}
