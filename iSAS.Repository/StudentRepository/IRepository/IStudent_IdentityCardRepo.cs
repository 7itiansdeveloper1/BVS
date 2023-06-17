using ISas.Entities.Student_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.StudentRepository.IRepository
{
    public interface IStudent_IdentityCardRepo
    {
        List<IdentityCard_StudentDetailsModel> Get_IdentityCard_StudentDetails(string ClassSecId, string ReportName, string sessionId);
    }
}
