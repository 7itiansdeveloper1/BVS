using ISas.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.Interface
{
    public interface IExam_MarksVerificationRepo
    {
        Exam_MarksVerificationModels GetStudentWiseMarksDetails(string userid, string sessionid, string classid, string sectionid, string erpno, string examid, string mainsubjectid);

        List<SelectListItem> GetDropDown_OnFormLoad(string SessionId, string UserId);
        List<SelectListItem> GetDropDownListByMode(Exam_MarksVerificationModels param);
        Exam_MarksVerificationModels StudentWiseMarksDetials(Exam_MarksVerificationModels param);
    }
}
