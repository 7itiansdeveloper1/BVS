using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISas.Entities.Exam_Entities;
namespace ISas.Repository.ExamRepository.IRepository
{
    public interface IExamReportcardTemplateRepo
    {
        ExamReportcardTemplateModels ExamReportcardTemplate_Transaction_FormLoad();
        ExamReportcardTemplateModels ExamReportcardTemplate_Transaction_GetTemplateList(string termId);
        Tuple<int, string> ExamReportcardTemplate_CRUD(ExamReportcardTemplateModels model);
        Tuple<int, string> Exam_TermLock_CRUD(string classId, string examId, bool value);
    }

}
