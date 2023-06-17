using ISas.Entities;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data;

namespace ISas.Repository.StudentRegistrationRepository.IRepository
{
    public interface IStudent_ReportRepo
    {
        Student_ReportModel GetStudentReport_FormLoad(string ModuleName, string ReportName, string UserId);

        Student_ReportModel GetStudentDetailReport(string ClassSectionId, string Reporttype, bool InActive, string OrderBy, string ReportName, string WingId,string sessionId);
        DataSet GetStudentDetailReport_Crystal(string reportValue, string sessionId, string userId, string filter1Value);
        List<SelectListItem> GetReportNameList(string ReportType, string userid);
    }
}
