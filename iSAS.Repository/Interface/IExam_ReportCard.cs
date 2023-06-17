using System.Collections.Generic;
using ISas.Entities;
using System.Data;
using System;

namespace ISas.Repository.Interface
{
    public interface IExam_ReportCard
    {
       IEnumerable<ClassStudentList> GetClassStudentList(string sessionid,string classid, string sectionid,string userid);
        IEnumerable<ClassStudentList> GetClassRetestStudentList(string sessionid, string classid, string sectionid, string userid);
        //IEnumerable<StudentCoScholasticMarkList> GetStudentCoScholasticMarkList(string examid, string userid, string classid, string sectionid, string subjectid, string erpno);
        //string SaveClassMarks(DataTable dt, string sessionid, string examid, string userid, string classid, string sectionid, string subjectid, string Assessmentid);
        //string Student_CoScholasticMarksEntry_CRUD(DataTable dt, string sessionId, string examId, string userId, string classId, string sectionId, string erpNo);
        DataSet GradeCard_ReportTemplate(string classId, string examId, string section, string erpNo, string session);
        DataSet GradeCard_GradeCard6to8(string classid, string sectionid, string sessionid, string erpno, string examid);
        DataSet MarkRegister_MarkRegister6to8(string classid, string sectionid, string sessionid, string erpno, string examid);
        DataSet GradeCard_GradeCard9to10(string classid, string sectionid, string sessionid, string erpno, string examid);
        DataSet MarkRegister_MarkRegister9to10(string classid, string sectionid, string sessionid, string erpno, string examid);

        DataSet MarkRegister_MarkRegister1to5(string classid, string sectionid, string sessionid, string erpno, string examid);
        DataTable GradeCard_ReportTemplate1(string classId, string examId);
        DataSet MarkRegister_MarkRegister11to12(string classid, string sectionid, string sessionid, string erpno, string examid);
        Tuple<int, string> Exam_Processing_Multi(string classid, string sectionid, string sessionid, string examid,string erpnos);
        Tuple<int, string> Exam_Processing_Single(string classid, string sectionid, string sessionid, string examid, string erpno);
    }
}
