using ISas.Entities.Examination_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.ExaminationRepository.IRepository
{
    public interface IExamination_AssessmentSetupRepo
    {
        List<SelectListItem> AssessmentNameList();
        List<Examination_AssessmentSetupModels> AssessmentList(string ExamTemplateId, int AssessmentPropertyId, string SubjectId);
        Examination_AssessmentSetupModels GetAssessmentById(string ExamTempleteId, int AssessmentPropertyId, string SubjectId);
        Tuple<int, string> Examination_AssessmentSetup_CRUD(Examination_AssessmentSetupModels model);
        Tuple<int, string> Examination_AssessmentSetup_CRUD(string AssessmentId);


        AssessmentPropertySetupModel GetAssessmentPropertyDetails(int AssessmentPropertyId);
        Tuple<int, string> Examination_AssessmentPropertySetup_CRUD(AssessmentPropertySetupModel model);
    }
}
