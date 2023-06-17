using ISas.Entities.DashboardEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.DashboardRepository.IRepository
{
    public interface IStudentHomeworkRepo
    {
        List<SelectListItem> StudentHomework_FormLoad(string erpNo, string sessionId);
        List<homework> StudentHomework_GetHomeWorkList(string erpNo, string sessionId, string fromdate, string todate, string hcategory, string subjectid,string status);
        homework StudentHomework_HomeWorkDetail(string erpNo, string homeworkId);
        Tuple<int, string> Student_HomeWorkMaster_CRUD(homework model,string mode);
    }
}
