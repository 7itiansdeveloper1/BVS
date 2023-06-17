using System.Collections.Generic;
using ISas.Entities;
using System.Data;
using System;

namespace ISas.Repository.Interface
{
    public interface IPtmAttendance
    {
        IEnumerable<PTMDatesList> GetPTMDatesList(string classid, string userid,string sessionid,string category);
        IEnumerable<StudentPTMAttendance> GetPTMClassStudent(string sessionid, string classid, string sectionid, string userid, string ptmdate);
        IEnumerable<CategoryList> GetPTMCategoryList();
        Tuple<int, string> SaveClassPTMAttendance(DataTable dt, string userid, string attdate,string sessionid);
        string GetClassTeacher(); 
    }
}
