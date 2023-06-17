using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
namespace ISas.Entities.DashboardEntities
{
    public class OnlineClass_Staff
    {
        public OnlineClass_Staff()
        {
            List<SelectListItem> classList = new List<SelectListItem>();
            List<SelectListItem> subjectList = new List<SelectListItem>();
        }
        //public string selectedClasssectionId { get; set; }
        //public string selectedsubjectId { get; set; }
        //public string classDate { get; set; }
        //public string classstartDateTime { get; set; }
        //public string classstartEndTime { get; set; }
        public List<SelectListItem> classList { get; set; }
        public List<SelectListItem> subjectList { get; set; }
        public onlineclass object_onlineclass { get; set; }

    }
    public class onlineclass
    {
        public Guid id { get; set; }
        [Display(Name = "Class Name")]
        public string onlineClassName { get; set; }
        [Display(Name ="Date")]
        public string onlineClassDate { get; set; }
        [Display(Name = "Class")]
        public string classId { get; set; }
        [Display(Name = "Section")]
        public string sectionId { get; set; }
        [Display(Name = "Subject")]
        public string subjectId { get; set; }
        public string teacherId { get; set; }
        [Display(Name = "Start Time")]
        public string onlineClassStartTime { get; set; }
        [Display(Name = "End Time")]
        public string onlineClassEndTime { get; set; }
        public bool isPostponed { get; set; }
        public bool IsDeleteable { get; set; }
        public string cDate { get; set; }
        public string zoomURL { get; set; }
    }

}
