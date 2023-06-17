using System.Collections.Generic;
using ISas.Entities;
using System.Data;
using System;

namespace ISas.Repository.Interface
{
   public interface IStudentTC
    {
        Tuple<int, string> StudentTC_CRUD(Student_TC model);
        Tuple<int, string> StudentTC1_CRUD(Student_TC model);
        IEnumerable<Student_TC> Student_TC_LandingPage(string TCID);
        IEnumerable<Student_TC> Student_TC1_LandingPage(string TCID);


        IEnumerable<Student_TC> GetStudentListForTC(string sessionid, string classid, string sectionid, string userid,string erpno);
        IEnumerable<TCSubjectList> GetTCSubjectList(string erpno = "");

        Student_TC  Student_TC_FormLoad();
        Student_TC Student_TC1_FormLoad();

        DataSet TC_Certificate(string erpno);
        DataSet TC_Certificate1(string erpno);

        Tuple<int, string> StudentTC_CRUD(string TCNo, string SessionId, string ERPNo);
        Tuple<int, string> StudentTC1_CRUD(string TCNo, string SessionId, string ERPNo);

        string getTCNumberByFilter(string admNo, string dob);
    }
}
