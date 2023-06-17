using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.UserEntities
{
    public class UserCreationModels
    {
        public UserCreationModels()
        {
            UserRoleList = new List<SelectListItem>();
            Not_RegisteredStaffOrStudent = new List<UserDetailsModels>();
            RegisteredStaffOrStudent = new List<UserDetailsModels>();
        }

        //[Required]
        [Display(Name ="Role")]
        public string RoleID { get; set; }

        [Display(Name ="User Type")]
        public string UserType { get; set; }

        public string Mode { get; set; }
        public string UserId { get; set; }
        public string SelectedUserOrStaffID { get; set; }

        public List<SelectListItem> UserRoleList {get;set;}
        public List<UserDetailsModels> Not_RegisteredStaffOrStudent { get; set; }
        public List<UserDetailsModels> RegisteredStaffOrStudent { get; set; }
    }

    public class UserDetailsModels
    {
        //public bool Selected { get; set; }
        public string StaffOrStudentId { get; set; }
        public string StaffOrStudent { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string UserRecoveryEmail { get; set; }
        public string UserMobileNo { get; set; }
        public bool IsAlreadyUser { get; set; }
    }
}
