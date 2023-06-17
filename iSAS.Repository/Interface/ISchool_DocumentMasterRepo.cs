using ISas.Entities.RegistrationEntities;
using System.Collections.Generic;

namespace ISas.Repository.Interface
{
    public interface ISchool_DocumentMasterRepo
    {
        IEnumerable<School_DocumentMaster> GetAllStudentDocumentList();
    }
}
