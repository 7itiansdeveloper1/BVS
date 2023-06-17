using ISas.Entities.Student_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.StudentRepository.IRepository
{
    public interface IMyClassRepo
    {
        MyClassModels MyClass_Transaction(string userid, string sessionid, string status);
        Tuple<int, string> UserCreation_CRUD(string userReferenceNo, string mode);
    }
}
