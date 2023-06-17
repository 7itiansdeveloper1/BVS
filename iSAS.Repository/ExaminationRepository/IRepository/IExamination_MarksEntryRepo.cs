using ISas.Entities.Examination_Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.ExaminationRepository.IRepository
{
    public interface IExamination_MarksEntryRepo
    {
        Examination_MarksEntryModels GetMarksEntryFormLoadDetails(string UserId);

        List<SelectListItem> GetClassList(string SessionId, string UserId, string ExamId);
        List<SelectListItem> GetSectionList(string SessionId, string UserId, string ExamId, string ClassId);
        List<SelectListItem> GetSubjectList(string SessionId, string UserId, string ExamId, string ClassId, string SectionId);
        List<SelectListItem> GetAssismentList(string SessionId, string UserId, string ExamId, string ClassId, string SectionId, string SubjectId);
        Examination_MarksEntryModels GetStudentDetails(Examination_MarksEntryModels param);
        Tuple<int, string> Examination_MarksEntry_CRUD(DataTable dt, Examination_MarksEntryModels model);
    }
}
