using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Entities.UserEntities
{
    public class UserPermissionModels
    {
        public UserPermissionModels()
        {
            ModuleList = new List<SelectListItem>();
            ModuleRoleList = new List<ModuleRoleDetailModel>();
        }

        public string ModuleID { get; set; }
        public string SelectedARefName { get; set; }
        public string UserReferenceNo { get; set; }
        public string UserId { get; set; }
        public string Mode { get; set; }
        public string UserType { get; set; }

        public List<SelectListItem> ModuleList { get; set; }
        public List<ModuleRoleDetailModel> ModuleRoleList { get; set; }
    }

    public class ModuleRoleDetailModel
    {
        public string RoleID { get; set; }
        public string MenuName { get; set; }
        public bool IsActive { get; set; }
        public bool canSAVE { get; set; }
        public bool canDELETE { get; set; }
        public bool canUPDATE { get; set; }
        public bool canVIEW { get; set; }
    }
}
