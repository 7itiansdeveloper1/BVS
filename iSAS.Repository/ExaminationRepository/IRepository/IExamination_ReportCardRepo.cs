using ISas.Entities.Examination_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.ExaminationRepository.IRepository
{
    public interface IExamination_ReportCardRepo
    {
        List<SelectListItem> Get_ReportCardDropDowns(string SessionId, string UserId, string ClassId, string Mode, string ExamId);
        Examination_ReportCardModels Get_ReportCardStudentDetails(string SessionId, string UserId, string ClassId, string SectionId, string ExamTempleteId);
        Examination_ReportCard_HtmlPrintModel Get_StudentReportDetails(Examination_ReportCardModels parm);
    }
}
