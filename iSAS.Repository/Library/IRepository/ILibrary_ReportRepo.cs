using ISas.Entities.LibraryEntities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.Library.IRepository
{
    public interface ILibrary_ReportRepo
    {
        Library_ReportModel Library_Transaction_GetReportList();
        Library_ReportModel Library_Transaction_GetReport(int reportId, string fromDate, string toDate, string sessionId);
        DataSet GetStudentDetailReport_Crystal(string reportValue, string sessionId, string userId, string filter1Value);

    }
}
