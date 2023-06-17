using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities;

namespace ISas.Repository.Interface
{
    public interface IExam_Result
    {
        IEnumerable<ExamNameList> GetExamList(string SessionId, string UserId);
        StudentGradeCardViewModel GetStudentGardeCardView(string examid, string erpno, string sessionid);
    }
}
