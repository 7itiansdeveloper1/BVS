using ISas.Entities.RegistrationEntities;
using System.Collections.Generic;

namespace ISas.Repository.Interface
{
    public interface ISchoolAdmissionCategoryRepo
    {
        IEnumerable<School_AdmissionCategoryMaster> GetAllAdmissionCategoryList();
    }
}
