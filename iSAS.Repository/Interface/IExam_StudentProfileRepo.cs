using ISas.Entities;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace ISas.Repository.Interface
{
    public interface IExam_StudentProfileRepo
    {
        List<SelectListItem> GetUserWiseClass(string UserId);
        List<SelectListItem> GetUserWiseSection(string UserId, string ClassId);

        List<StudentDetailModel> Profile_Attendnace_Cascading(string userid, string sessionid, string classid, string sectionid, string examid, string mode);
        string Profile_Attendnace_CRUD(string userid, string sessionid, string classid, string sectionid, string examid, DataTable dtVal);
    }
}
