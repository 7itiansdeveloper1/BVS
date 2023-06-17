using ISas.Entities.Academic;
using System.Collections.Generic;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_HolidayDeclarationRepo
    {
        List<Academic_HolidayDeclarationModel> GetHolidayDeclarationList(string HolidayId);
        Academic_HolidayDeclarationModel GetHolidayDeclarationById(string HolidayId);
        string Academic_HolidayDeclaration_CRUD(Academic_HolidayDeclarationModel model);
        string Academic_HolidayDeclaration_CRUD(string HolidayId);
    }
}
