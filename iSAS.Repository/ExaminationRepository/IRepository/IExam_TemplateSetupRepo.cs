using ISas.Entities.Exam_Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.ExaminationRepository.IRepository
{
    public interface IExam_TemplateSetupRepo
    {
        List<Exam_TemplateSetupModels> GetExamTempleteList(string Exam_TemplateId);
        Exam_TemplateSetupModels GetExamTempleteById(string TempleteId);
        Tuple<int, string> Exam_TemplateSetup_CRUD(Exam_TemplateSetupModels model);
        Tuple<int, string> Exam_TemplateSetup_CRUD(string TemplateId);

        List<SelectListItem> TempleteClassSetupDetails(string Exam_TemplateId);
        Tuple<int, string> TempleteClassSetup_CRUD(Exam_Template_ClassSetupModels model);
        Exam_Template_GradingSetupModels TempleteGradingSetup_FormLoad(string Exam_TemplateId);
        Tuple<int, string> TempleteGradingSetup_CRUD(Exam_Template_GradingSetupModels model);
    }
}
