using System;
using System.Collections.Generic;
using ISas.Entities;
using System.Data;

namespace ISas.Repository.Interface
{
   public interface IStudentAttendance
    {
        //string GetClassTeacher(string studentsession, string studentclass, string studentsection, string userid, DateTime attdate); 
        IEnumerable<ClassPhotoList> GetClassPhotoList(string studentsession,string studentclass,string studentsection,string userid);
        Tuple<IEnumerable<StudentAttendance>, string, bool> GetClassStudent(string studentsession, string studentclass, string studentsection, string userid, DateTime attdate);
        Tuple<int, string> SaveClassAttendance( DataTable dt,string userid, DateTime attdate,string sessionid, bool sendmessage);
        IEnumerable<DailyAttenanceSummary> GetDailyAttendanceSummary(string studentsession, DateTime attdate);
        Tuple<int, string> UploadStudentPhoto(string refno, string imageurl, string reftype);
        Tuple<int, string> DeleteClassAttendance(string classId, DateTime attdate, string sessionid, string sectionId);
    }
}
