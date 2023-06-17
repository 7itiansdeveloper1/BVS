using ISas.Entities.Exam_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.ExamRepository.IRepository
{
    public interface IExam_ClassSubjectSetupRepo
    {
        Exam_ClassSubjectSetup ClassSubjectSetup(string function);
        List<SelectListItem> ClassSubjectSetup_assessment(string function, string classsectionid, string subjectid);
        List<ClassSubject> ClassSubjectSetup_classSubject(string function, string classsectionid);
        Tuple<int, string> ClassSubjectSetup_CRUD(Exam_ClassSubjectSetup model);
    }
}
