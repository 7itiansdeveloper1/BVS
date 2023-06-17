using ISas.Entities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Interface
{
    public interface IStudent_NSORepo
    {
        DropDownListFor_Student_NSO GetStudent_NSODropDownList(string UserId, string ClassID,string SectinID);
        Tuple<int, string> Student_NSO_CRUD(Student_NSOModel model,string userid);
        IEnumerable<Student_NSOModel> GetAllStudent_NSOList(string NSONo = "");
        Student_NSOModel GetStudent_NSONSONo(string NSONo);
    }
}
