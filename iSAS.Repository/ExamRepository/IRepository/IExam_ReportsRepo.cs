using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ISas.Entities.Exam_Entities;
namespace ISas.Repository.ExamRepository.IRepository
{
    public interface IExam_ReportsRepo
    {
        List<SelectListItem> Exam_Report_Transaction_GetReportList(string sessionId, string userId);
        List<SelectListItem> Exam_Report_Transaction_GetClassList(string sessionId, string reportId, string userId);
        List<SelectListItem> Exam_Report_Transaction_GetSubjectList(string sessionId, string reportId, string classSectionId, string userId);
        Exam_ReportModels GetReportData(string sessionId, string reportId, string classSectionId, string subjectId, string userId);
     }
}
