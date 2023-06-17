using ISas.Entities.Examination_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.ExaminationRepository.IRepository
{
    public interface IExamination_SubjectMasterRepo
    {
        Examination_SubjectMasterModels SubjectMaster_FormLoad();
        List<Examination_SubjectMasterModels> GetSubjectMstList(string ExamTempleId, string SubjectId);
        Examination_SubjectMasterModels GetSubjectMstById(string ExamTempleId, string SubjectId);
        Tuple<int, string> Examination_SubjectMaster_CRUD(Examination_SubjectMasterModels model);
        Tuple<int, string> Examination_SubjectMaster_CRUD(int PropertyId);


        Examination_SubjectPropertyModel GetSubjectSetupPropertyDetails(string PropertyId);
        Tuple<int, string> Examination_SubjectPropertySetup_CRUD(Examination_SubjectPropertyModel model);
    }   
}
