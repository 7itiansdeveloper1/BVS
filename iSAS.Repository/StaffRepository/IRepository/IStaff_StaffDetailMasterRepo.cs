using ISas.Entities.StaffEntities;
using ISas.Entities.TimeTable_Entities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.StaffRepository.IRepository
{
    public interface IStaff_StaffDetailMasterRepo
    {
        DropDownListFor_Staff_StaffDetailMaster GetStaffDetailMasterDropDownList(string CorresCountryID,
            string CorresStateID, string PermCountryID, string PermStateID);
        Tuple<int, string> Staff_StaffDetailMaster_CRUD(Staff_StaffDetailMasterModel model);
        IEnumerable<Staff_StaffDetailMasterModel> GetAllStaff_StaffDetailMasterList(string StaffID, string IncludeInactive);
        Staff_StaffDetailMasterModel GetStaff_StaffDetailMasterByStaffID(string StaffID);
        string GetStaffIDByUserID(string UserID);
        StaffProfileDetailsModel GetStaffProfileDetails(string UserID);
        List<TeacherDetailModel> GetTeacherList();
        TimeTable_SetupModels GetStaffTimeTable(string StaffId);
    }
}
