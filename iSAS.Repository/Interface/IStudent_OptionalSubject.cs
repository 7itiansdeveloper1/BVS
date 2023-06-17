using System.Collections.Generic;
using ISas.Entities;
using System.Data;
using System;
using System.Web.Mvc;

namespace ISas.Repository.Interface
{
    public interface IStudent_OptionalSubject
    {
        IEnumerable<OptionalSubjectParametersList> GetOptionalSubjectParametersList(string sessionid, string classid, string userid);
        Tuple<List<StudentList>, List<SelectListItem>> GetStudentList(string sessionid, string classid, string sectionid, string userid, string mode);
        IEnumerable<OptionalSubjectList> GetOptionalSubjectList(string sessionid, string classid, string sectionid, string userid, string mode);
        Tuple<int, string> Student_OptionalSubject_CRUD(DataTable dt, string userid,string sessionid, string mode);
    }
}
