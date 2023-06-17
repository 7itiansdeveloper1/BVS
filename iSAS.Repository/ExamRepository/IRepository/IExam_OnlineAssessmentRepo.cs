using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.Exam_Entities;
namespace ISas.Repository.ExamRepository.IRepository
{
    public interface IExam_OnlineAssessmentRepo
    {
        //Exam_OnlineAssessmentModels Exam_OnlineAssessment_FormLoad(string erpNo);
        List<Exam_OnlineAssessmentModels> Exam_OnlineAssessment_FormLoad(string erpNo);
        Exam_QuestionMasterModels Exam_BrowseAssessment(int qpId, int qNo, string subjectName, int maxMark, string userId);
        void Exam_StudentResult_CRUD(string erpNo, int qId, string ans);
        void Exam_StudentDisriptiveResult_CRUD(string erpNo, int qId, string ans);
        Tuple<int, string> AnswerSheetUpload_CRUD(int qpId, string ansDocPath, string erpNo, string mode);
    }

}
