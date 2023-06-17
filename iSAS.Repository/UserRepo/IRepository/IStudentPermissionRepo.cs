using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.UserEntities;

namespace ISas.Repository.UserRepo.IRepository
{
    public interface IStudentPermissionRepo
    {
        StudentRoleModels StudentRoleAssign_Transaction_GetRoleList(string classId);
        StudentRoleModels StudentRoleAssign_Transaction_FormLoad();
        string StudentRoleAssign_CRUD(StudentRoleModels model);
    }
}
