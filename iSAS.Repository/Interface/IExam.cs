using System.Collections.Generic;
using ISas.Entities;
using System.Data;
using System;

namespace ISas.Repository.Interface
{
    public interface IExam
    {
        IEnumerable<ExamNameList> GetExamNameList(string SessionId, string UserId,string mode);
        IEnumerable<ExamNameList> GetExamNameList_Retest(string SessionId, string UserId);
        IEnumerable<ClassList> GetClassList(string examId, string userid);
        IEnumerable<SectionList> GetSectionList(string examId, string classId, string userid);
        IEnumerable<SubjectList> GetSubjectList(string examId, string userid, string classid, string sectionid);
        IEnumerable<GradingList> GetGradeList(string classid, string subjectid);
        IEnumerable<AssessmentNameList> GetAssessmentList(string examid, string userid, string classid, string sectionid, string subjectid);
        IEnumerable<StudentsMarkList> GetStudentMarkList(string examid, string userid, string classid, string sectionid, string subjectid, string AssessmentId,string sessionId);
        IEnumerable<StudentsMarkList> GetRetestStudentMarkList(string examid, string userid, string classid, string sectionid, string subjectid, string assessmentid, string sessionId);

        IEnumerable<ClassStudentList> GetClassStudentList(string userid, string classid, string sectionid, string subjectid, string sessionid);
        IEnumerable<ParentSubjectList> GetParentSubjectList(string examid, string userid, string classid, string sectionid);
        IEnumerable<StudentCoScholasticMarkList> GetStudentCoScholasticMarkList(string examid, string userid, string classid, string sectionid, string subjectid, string erpno);
        IEnumerable<StudentActivityMarkList> GetStudentActivityMarkList1(string examid, string assessmentid, string userid, string classid, string sectionid, string subjectid, string sessionId);
        DataTable MaxMarkConfiguration(string examid, string classid, string sectionid, string subjectid, string assessmentid);

        DataSet GetActivityHeaderList(string examid, string userid, string classid, string sectionid, string subjectid, string assessmentId);
        Tuple<int, string> SaveClassMarks(DataTable dt, string sessionid, string examid, string userid, string classid, string sectionid, string subjectid, string Assessmentid);
        Tuple<int, string> SaveClassMarks_Retest(DataTable dt, string sessionid, string examid, string userid, string classid, string sectionid, string subjectid, string Assessmentid);
        string Student_CoScholasticMarksEntry_CRUD(DataTable dt, string sessionId, string examId, string userId, string classId, string sectionId, string erpNo);
        Tuple<int, string> Student_ActivityMark_CRUD(ActivityMarksEntryModel model);
        
        Exam_MarksVerificationModel GetClassVerificationList(string sessionid, string classid, string sectionid, string examid, string subjectid);
        Exam_MarksVerificationCoScholasticModels GetClassVerificationCoScholasticList(string sessionid, string classid, string sectionid, string examid, string subjectid);
        DataSet GradeCard_GradeCard1to2(string classid, string sectionid, string sessionid, string erpno, string examid);
        IEnumerable<ExamNameList> GetExamNameListForMR(string SessionId, string UserId);

        Tuple<int, string> RetestStudent_CRUD(string sessionId, string erpNo, string userId, bool value, string subjectId, string assessmentId);
        DataTable RetestMaxMarkConfiguration(string examid, string classid, string sectionid, string subjectid, string assessmentid);
        IEnumerable<ClassList> GetClassListForReportCard(string examid, string userid, string sessionId);
    }
}
