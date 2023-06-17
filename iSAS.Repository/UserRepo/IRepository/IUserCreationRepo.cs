using ISas.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.UserRepo.IRepository
{
    public interface IUserCreationRepo
    {
        UserCreationModels UserCreation_FormLoad(string UserType);
        //Tuple<int, string> UserCreation_CRUD(UserCreationModels model);
        Tuple<int, string> UserCreation_CRUD(string userReferenceNo, string userType, string roleId, string userId, string mode);
    }
}
