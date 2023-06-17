using ISas.Entities.SMSManagement;
using System;
using System.Collections.Generic;
using System.Data;

namespace ISas.Repository.SMSManagement.IRepository
{
    public interface ISMSManagementRepo
    {
        DropDownListFor_SMSManagement GetSMS_ManagementDropDownList();
        List<SMS_StudentModel> GetStudentDetails(string ClassIds, string StudentGroupId, string SessionId);
        List<SMS_AdminAndStaffModel> GetStaffDetails(string DeptIds, string StaffGroupId, string IsForAdminStaff);

        Tuple<int, string> StudentSMSGroup_CRUD(string GroupName, string SMSGroupType, List<SMS_StudentModel> studentList);
        Tuple<int, string> StaffSMSGroup_CRUD(string GroupName, string SMSGroupType, List<SMS_AdminAndStaffModel> staffList);
        List<SMS_OutboxModel> GetSMSOutboxDetails();
        Tuple<int, string> Delete_OutBoxSMS(DataTable dt);
    }
}
