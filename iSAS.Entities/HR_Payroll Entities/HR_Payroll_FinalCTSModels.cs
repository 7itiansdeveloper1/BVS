using System.Collections.Generic;

namespace ISas.Entities.HR_Payroll_Entities
{
    public class HR_Payroll_FinalCTSModels
    {
        public HR_Payroll_FinalCTSModels()
        {
            StaffDetails = new List<HR_Payroll_FinalCTSStaffDetailModel>();
            HeadNameList = new List<string>();
            FinalCTSList = new List<HR_Payroll_FinalCTSStaffDetailModel>();
        }

        public string PayBandId { get; set; }
        public string PayBandName { get; set; }
        public List<HR_Payroll_FinalCTSStaffDetailModel> StaffDetails { get; set; }

        public List<HR_Payroll_FinalCTSStaffDetailModel> FinalCTSList { get; set; }

        public List<string> HeadNameList { get; set; }
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }

    public class HR_Payroll_FinalCTSStaffDetailModel
    {
        public HR_Payroll_FinalCTSStaffDetailModel()
        {
            PayBandCTSDetails = new List<int>();
        }
        public bool Selected { get; set; }
        public string StaffId { get; set; }
        public string StaffName { get; set; }
        public List<int> PayBandCTSDetails { get; set; }
    }
}
