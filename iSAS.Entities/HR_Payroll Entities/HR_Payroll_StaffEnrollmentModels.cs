using System.Collections.Generic;

namespace ISas.Entities.HR_Payroll_Entities
{
    public class HR_Payroll_StaffEnrollmentModels
    {
        public HR_Payroll_StaffEnrollmentModels()
        {
            NonEnrolledStaffList = new List<HR_Payroll_StaffDetailsModels>();
            EnrolledStaffList = new List<HR_Payroll_StaffDetailsModels>();
        }

        public string PayBandId { get; set; }
        public string PayBandName { get; set; }

        public string UserId { get; set; }
        public string CRUDMode { get; set; }

        public List<HR_Payroll_StaffDetailsModels> NonEnrolledStaffList { get; set; }
        public List<HR_Payroll_StaffDetailsModels> EnrolledStaffList { get; set; }
    }

    public class HR_Payroll_StaffDetailsModels
    {
        public bool Selected { get; set; }
        public string StaffID { get; set; }
        public string StaffName { get; set; }
        public string DeptName { get; set; }
        public bool IsDeleteable { get; set; }
    }
}
