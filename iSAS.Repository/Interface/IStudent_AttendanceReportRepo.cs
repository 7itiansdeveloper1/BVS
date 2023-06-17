using ISas.Entities;

namespace ISas.Repository.Interface
{
    public interface IStudent_AttendanceReportRepo
    {
        Student_AttendanceReportModels Student_AttendanceReport_FormLoad(string ModuleName, string ReportName);
        Student_AttendanceReportModels Student_AttendanceReport(string fromdate, string todate, string ClassSectionId, string InActive, string ReportName, string SessionId, string StudentId);
        //Student_AttendanceReportModels Student_AttendanceDetailReport(string fromdate, string todate, string ClassSectionId, bool InActive, string ReportName, string SessionId, string StudentId);
    }
}
