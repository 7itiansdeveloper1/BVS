using ISas.Entities.DashboardEntities;
using System;
using System.Collections.Generic;
using System.Data;

namespace ISas.Repository.DashboardRepository.IRepository
{
    public interface IDashboardRepo
    {
        Dashboard_StudentModel GetDashBoard_StudentDetails(string UserId, string UserRole, string Date, string SessionId);
        List<Tuple<string, string>> GetStudentAttenDetails(int Month, int Year, string UserID);
        List<AttnDetailsModel> GetStudentAttenDetails_BySession(int SessionId, string UserID);

        DashboardModel_Admin GetAdminDashboardDetails(string Date, string SessionId,string UserId);
        DashboardModel_Admin GetAdminDashboardDetails_Atten(string Date, string SessionId);
        DataSet GetAdminDashboardDetails_Strength(string Date, string SessionId);
        List<Tuple<string, string>> GetStudentAttenDetails_ByERPNo(int Month, int Year, string UserID);

        DataSet GetAdminDashboardDetails_StaffAtten(string Date, string SessionId);
        Dashboard_StaffModel GetDashBoard_StaffDetails(string UserId, string SessionId);
        List<AttendanceDetailModel> AttendanceDetails(string ERPNo, string SessionId);
        string GetAttendanceDefaulterMessage(string ERPNo, string SessionId);
    }
}
