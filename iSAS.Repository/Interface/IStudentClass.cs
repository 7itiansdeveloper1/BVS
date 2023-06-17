using System.Collections.Generic;
using System.Web.Mvc;
using ISas.Entities;


namespace ISas.Repository.Interface
{
   public interface IStudentClass
    {
        IEnumerable<StudentClass> GetAllClasses(string userid);
        IEnumerable<StudentClass> GetAllClasses();
        //IEnumerable<Student> GetStudentListForTC(string sessionid, string classid, string sectionid, string userid);
        //string StudentTC_CRUD(Student_TC model);
        //IEnumerable<Student_TC> Student_TC_FormLoad(string TCID);
        //IEnumerable<Student_TC> Student_NEWTC_FormLoad();
    }
}
