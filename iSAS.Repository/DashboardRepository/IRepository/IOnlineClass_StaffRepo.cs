using ISas.Entities.DashboardEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.DashboardRepository.IRepository
{
    public interface IOnlineClass_StaffRepo
    {
        OnlineClass_Staff OnlineClasses_Transaction_FormLoad(string userid);
        Tuple<int, string> OnlineClasses_CRUD(onlineclass model);
        List<onlineclass> OnlineClasses_Transaction_ClassList(string userid, string classdate);
        Tuple<int, string> ZoomURL_CRUD(string userid, string zoomurl);
    }
}
