using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.UserEntities
{
    public class StudentRoleModels
    {

        public StudentRoleModels() {
            studentRoleAssignList = new List<StudentRoleAssignList>();
            classList = new List<SelectListItem>();
         }
        [Display(Name = "Class")]
        public string classId { get; set; }
        public string userId { get; set; }
        public string rolesId { get; set; }
        public List<StudentRoleAssignList> studentRoleAssignList { get; set;}
        public List<SelectListItem> classList { get; set; }
    }

    public class StudentRoleAssignList
    {
        public Boolean isSelected { get; set; }
        public string roleId { get; set; }
        public string displayRoleName { get; set; }
        public int displayOrder { get; set; }
        
    }
}
