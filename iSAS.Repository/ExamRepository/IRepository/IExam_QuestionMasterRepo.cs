using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.Exam_Entities;
namespace ISas.Repository.ExamRepository.IRepository
{
    public interface IExam_QuestionMasterRepo
    {
        List<Exam_QuestionMasterModels> QuestionMasterFormLoad();
        Tuple<int, string> QuestionMaster_CRUD(Exam_QuestionMasterModels model, string userId);
        Exam_QuestionMasterModels Get_QuestionDetails(string qpId, string className, string subjectName, string assessmentName, int maxMark, int qid);
    }
}
