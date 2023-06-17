using ISas.Entities.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_HomeWorkMasterRepo
    {
        List<Academic_HomeWorkMasterModels> Get_Academic_HomeWorkMasterList(string SessionId, string UserId, string CategoryId);
        Academic_HomeWorkMasterModels Get_HomeWorkMasterById(string HomeWorkId, string SessionId, string UserId);
        Academic_HomeWorkMasterModels Get_Academic_HomeWorkMaster_FormLoad(string SessionId, string UserId);
        List<SelectListItem> Get_StudentListByClassSectionId(string ClassSectionId, string SessionId, string UserId);
        Tuple<int, string> Academic_HomeWorkMaster_CRUD(Academic_HomeWorkMasterModels model);
        Tuple<int, string> Academic_HomeWorkMaster_CRUD(string HomeWorkId, string ToBeRemovedAttach, string AllAttachments, string UserId,string mode);
        Tuple<int, string> Academic_HomeWorkMaster_DELETE(string HomeWorkId);
        List<Academic_HomeWorkMasterModels> Get_Academic_HomeWorkMasterList(string Date, string UserId, string ERPNo, string Category);
        Academic_HomeWorkMasterModels getResponseList(string homeworkid, string sessionid,string responselistname,string userid);
        answerSheet getAnswersheet(string homeworkid, string studentid, string studentname, string homeworkname);
        void UpdateReview(string homeworkid, string studentid, bool isreviewed);
        Tuple<int, string> Teacher_Answersheet_CRUD(string homeworkid, string studentid, string revertAttachments, string remark, string mode);
        void UpdateSubmitStatus(string homeworkid, string studentid, bool issubmitted);
    }
}
