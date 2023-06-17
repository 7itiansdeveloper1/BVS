using ISas.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.UserRepo.IRepository
{
    public interface IUserPermissionRepo
    {
        List<SelectListItem> ModuleList();
        UserPermissionModels ModuleRoleList(string ModuleID, string UserRefID);
        string UserPermission_CRUD(UserPermissionModels model);
        Tuple<int, string> UserRoleMaster_CRUD(string roleId, bool isActive, string userId);
    }
}
