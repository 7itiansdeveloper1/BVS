using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Entities.Academic
{
    public class Academic_HolidayAllocationModel
    {
        public Academic_HolidayAllocationModel()
        {
            ClassList = new List<SelectListItem>();
            DepartmentList = new List<SelectListItem>();
            StaffList = new List<StaffDetailModel>();
        }

        public long SNo { get; set; }
        public string HolidayId { get; set; }
        public string HolidayName { get; set; }
        public string FDate { get; set; }
        public string TDate { get; set; }
        public string Wings { get; set; }
        public string Department { get; set; }
        public int NoofDays { get; set; }
        public bool IsDeletable { get; set; }

        //Extra Feild
        public string CRUDFor { get; set; }
        public string CRUDMode { get; set; }

        public List<SelectListItem> ClassList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<StaffDetailModel> StaffList { get; set; }
    }
    public class StaffDetailModel
    {
        public bool Selected { get; set; }
        public string StaffID { get; set; }
        public string StaffName { get; set; }
        public string DeptId { get; set; }
        public string DeptName { get; set; }
    }
}
