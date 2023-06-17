using ISas.Entities.Examination_Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace ISas.Repository.ExaminationRepository.IRepository
{
    public interface IExamination_ProfileEntryRepo
    {
        List<SelectListItem> Get_ProfileEntryDropDowns(string SessionId, string UserId, string ClassId, string Mode, string ExamId);
        Examination_ProfileEntryModels Get_ProfileEntryStudentDetails(string SessionId, string UserId, string ClassId, string SectionId, string ExamTempleteId);
        Tuple<int, string> Examination_ProfileEntry_CRUD(DataTable dt, Examination_ProfileEntryModels model);
    }
}
