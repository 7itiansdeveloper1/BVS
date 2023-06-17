using System.Collections.Generic;
using ISas.Entities;
using System.Data;
using System;

namespace ISas.Repository.Interface
{
    public interface IExam_AchievementRepository
    {
        IEnumerable<ExamNameList> GetExamNameList(string userId);
        IEnumerable<StudentAchievementList> GetStudentAchievementList(string sessionId, string classId, string sectionId, string examId);
        //string SaveClassMarks(DataTable dt, string sessionid, string examid, string userid, string classid, string sectionid, string subjectid, string Assessmentid);
        Tuple<int, string> Exam_Achievement_CRUD(DataTable dt, string sessionId, string examId, string userId);
    }
}
