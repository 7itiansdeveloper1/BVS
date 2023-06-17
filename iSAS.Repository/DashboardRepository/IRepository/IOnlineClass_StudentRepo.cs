using ISas.Entities.DashboardEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.DashboardRepository.IRepository
{
    public interface IOnlineClass_StudentRepo
    {
        //List<OnlineClassesModel> GetStudentOnlineClassList(string userid);
        OnlineClassesModel GetStudentOnlineClassList(string userid);
        Tuple<bool, int, string> onlineLogPunch_CRUD(string erpno, string subjectid);
    }
}
