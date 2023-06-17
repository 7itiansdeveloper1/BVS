using ISas.Entities.RegistrationEntities;
using System;
using System.Collections.Generic;
using System.Data;
namespace ISas.Repository.StudentRegistrationRepository.IRepository
{
    public interface IStudentRegistrationRepo
    {
        Tuple<int, string> Student_Registration_CRUD(Student_RegistrationMaster model);
        IEnumerable<Student_RegistrationMaster> Student_Registration_MasterDetails(string RegID);
        Student_RegistrationMaster GetStudentRegistrationByRegID(string RegID);
        Tuple<string, string> GetReleatedAutoNos(string SessionId);
        RegistrationSlipModel RegistrationSlipDetails(string RegId);
        //DataTable ValidateDOB(string ClassId);
        DataTable ValidateDOB(string ClassId, string DOB, DateTime AOD);
    }
}
