using ISas.Entities.RegistrationEntities;
using System.Collections.Generic;

namespace ISas.Repository.Interface
{
    public interface ICityRepository
    {
        IEnumerable<School_CityMaster> GetCityListByStateID(string StateID);
    }
}
