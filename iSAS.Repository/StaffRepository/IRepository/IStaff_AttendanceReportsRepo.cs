using ISas.Entities.StaffEntities;
using System.Collections.Generic;

namespace ISas.Repository.StaffRepository.IRepository
{
    public interface IStaff_AttendanceReportsRepo
    {
        Staff_AttendanceReportsModels Staff_AttendanceReports_FormLoad();
        Staff_AttendanceReportsModels GetStaff_AttendanceReports(string StaffIds, string Reporttype, bool InActive, string OrderBy, string ReportName, string ReportFilterBy, string FromDate, string ToDate);
    }
}
