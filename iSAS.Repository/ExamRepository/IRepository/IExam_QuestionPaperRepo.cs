using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.Exam_Entities;

namespace ISas.Repository.ExamRepository.IRepository
{
    public interface IExam_QuestionPaperRepo
    {
        Exam_QuestionPaperModels QuestionPaper_FormLoad(string sessionId, string userId);
        Tuple<int, string> QuestionPaper_CRUD(int qpId, string classId, string subjectId, string assessmentId, int maxmark, string userId, bool isActive);
        Tuple<int, string> QuestionPaperUpload_CRUD(int qpId, string docPath, string userId,string mode);
        Exam_AnswersheetModels Exam_Answersheet_TRANSACTION(string sessionId, string userId, int qpId, string className, string assessmentName, string subjectName);
    }
}
