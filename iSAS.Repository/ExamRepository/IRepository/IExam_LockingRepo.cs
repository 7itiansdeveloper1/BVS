using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.Exam_Entities;


namespace ISas.Repository.ExamRepository.IRepository
{
    public interface IExam_LockingRepo
    {
        Exam_LockingModels Exam_LockingModels_Cascading();
        List<classAssessmentList> Exam_LockingModels_Cascading(string classId);
        Tuple<int, string> Exam_AssessmentLock_CRUD(string classId, string assId, bool value);
    }
}
