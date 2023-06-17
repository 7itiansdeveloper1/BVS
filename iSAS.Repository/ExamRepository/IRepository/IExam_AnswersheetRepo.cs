using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.Exam_Entities;

namespace ISas.Repository.ExamRepository.IRepository
{
    public  interface IExam_AnswersheetRepo
    {
        StudentAssessmentResult GetAnswersheet(string erpNo, int qpId);
    }
}
