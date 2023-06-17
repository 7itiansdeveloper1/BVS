using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.StaffEntities
{
    public class Staff_AttendanceRegisterModels
    {
        public Staff_AttendanceRegisterModels()
        {
            DeparmentList = new List<SelectListItem>();
            StaffAttendanceDetails = new List<StaffAttendanceDetailsModel>();
        }

        [Display(Name ="Attendance Date")]
        [Required(ErrorMessage = "Attendance Date is Req..")]
        public string AttenDate { get; set; }

        [Required(ErrorMessage = "Departments is Req..")]
        [Display(Name = "Departments")]
        public string DepartmentIds { get; set; }


        public List<SelectListItem> DeparmentList { get; set; }
        public List<StaffAttendanceDetailsModel> StaffAttendanceDetails { get; set; }

        public string SessionId { get; set; }
        public string UserId { get; set; }
    }

    public class StaffAttendanceDetailsModel
    {
        public string StaffID { get; set; }
        public string StaffCode { get; set; }
        public string StaffName { get; set; }
        public string Department { get; set; }
        public bool AttStatus { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string Mobile { get; set; }
        public string AttStatusStr { get; set; }
        
        
    }
}
