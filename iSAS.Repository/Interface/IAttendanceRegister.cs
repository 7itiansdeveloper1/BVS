using System.Collections.Generic;
using ISas.Entities;
using System.Data;


namespace ISas.Repository.Interface
{
    public interface IAttendanceRegister
    {
        IEnumerable<AttendanceRegisterMonthList> GetAttendanceRegisterMonthList(string userid, string sessionid,string classid, string sectionid);
        AttendanceRegisterViewModel GetClassAttendanceRegister(string sessionid, string classid, string sectionid, string monthname);
        List<StudentAttendanceDetailsModel> StudentAttenDetails(string sessionid, string classid, string sectionid, string monthname);
    }
}
