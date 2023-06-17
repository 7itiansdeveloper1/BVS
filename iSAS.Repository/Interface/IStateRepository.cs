using ISas.Entities.RegistrationEntities;
using System.Collections.Generic;

namespace ISas.Repository.Interface
{
    public interface IStateRepository
    {
        IEnumerable<School_StateMaster> GetStateListByCountryID(string CountryID);
    }
}
