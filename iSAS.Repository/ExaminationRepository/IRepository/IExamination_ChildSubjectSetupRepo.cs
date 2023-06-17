using ISas.Entities.Examination_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.ExaminationRepository.IRepository
{
    public interface IExamination_ChildSubjectSetupRepo
    {
        List<Examination_ChildSubjectSetupModels> GetChildSubjectList(string ExamTempleId, string SubjectId, int SubjectPropertyId);
        Examination_ChildSubjectSetupModels GetChildSubjectById(string ExamTempleId, string SubjectId, int SubjectPropertyId);
        Tuple<int, string> Examination_ChildSubjectSetup_CRUD(Examination_ChildSubjectSetupModels model);
        Tuple<int, string> Examination_ChildSubjectSetup_CRUD(int PropertyId);
    }   
}
