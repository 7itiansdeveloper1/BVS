using ISas.Entities.RegistrationEntities;
using System.Collections.Generic;

namespace ISas.Repository.Interface
{
    public interface ICountryRepository
    {
        IEnumerable<School_CountryMaster> GetAllCountryList();
    }
}
