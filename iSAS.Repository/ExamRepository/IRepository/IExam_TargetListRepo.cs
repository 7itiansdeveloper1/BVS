using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ISas.Entities.Exam_Entities;
namespace ISas.Repository.ExamRepository.IRepository
{
    public interface IExam_TargetListRepo
    {
        List<SelectListItem> TargetListXI_Transaction_GetClassList(string userid, string sessionid, string classSectionId, string subjectId);
        List<SelectListItem> TargetListXI_Transaction_GetSubjectList(string userid, string sessionid, string classSectionId, string subjectId);
        Exam_TargetModels TargetListXI_Transaction_GetTargetList(string userid, string sessionid, string classSectionId, string subjectId, string subjectName, string className);
    }
}
