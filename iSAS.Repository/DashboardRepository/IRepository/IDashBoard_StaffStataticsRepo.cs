using ISas.Entities.DashboardEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.DashboardRepository.IRepository
{
    public interface IDashBoard_StaffStataticsRepo
    {
        MyClassInfoModel GetClassInfo(string SessionId, string UserId);
        List<BookHistoryModel> GetBookHistory(string SessionId, string UserId);
        List<SMSDetailsModel> GetSMSDetails(string SessionId, string UserId);
        List<SalaryDetailsModel> GetSalaryDetails(string SessionId, string UserId);
        Staff_AttendanceDetailsModel GetStaffAttendanceInfo_FormLoad(string UserId, int Month, int Year);
        List<Tuple<string, string>> GetStaffAttenDetails(int Month, int Year, string UserID);
        Tuple<int, string> DashBoard_StaffStatatics_CRUD(Staff_AttendanceDetailsModel model);
        List<LeaveBalanceDetailsModel> GetStaffLeaveBalanceDetails(string UserId, int Month, int Year);
    }
}
