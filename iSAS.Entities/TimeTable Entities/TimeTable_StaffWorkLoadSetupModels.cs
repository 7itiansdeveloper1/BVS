using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.TimeTable_Entities
{
    public class StaffClassSetupModels
    {
        public StaffClassSetupModels()
        {
            ClassList = new List<SelectListItem>();
        }

        public string StaffId { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public string UserId { get; set; }
    }

    public class StaffSubjectSetupModels
    {
        public StaffSubjectSetupModels()
        {
            SubjectList = new List<SelectListItem>();
            ClassSecList = new List<SelectListItem>();
        }
        public string StaffId { get; set; }

        [Display(Name = "Class")]
        public string ClassSecId { get; set; }
        public List<SelectListItem> ClassSecList { get; set; }
        public List<SelectListItem> SubjectList { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
    }
}
