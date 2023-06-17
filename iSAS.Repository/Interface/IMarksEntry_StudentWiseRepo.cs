using ISas.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.Interface
{
    public interface IMarksEntry_StudentWiseRepo
    {
        Tuple<List<SelectListItem>, List<SelectListItem>> GetMainSubjectWithStudentList(string UserId, string ExamId, string ClassId, string SectionId,string sessionId);

        MarksEntryStudentWiseModel GetStudentWiseMarkList(string userid, string examid, string classid, string sectioinid, string mainsubjectid, string sessionid, string erpno);

        string SubjectWiseMarksEntry_CRUD(MarksEntryStudentWiseModel model);
    }
}
