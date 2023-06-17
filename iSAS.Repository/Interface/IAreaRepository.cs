using ISas.Entities.RegistrationEntities;
using System.Collections.Generic;

namespace ISas.Repository.Interface
{
    public interface IAreaRepository
    {
        IEnumerable<School_AreaMaster> GetAllAreaList();
    }
}
