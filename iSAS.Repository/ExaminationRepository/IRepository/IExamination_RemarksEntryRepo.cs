using ISas.Entities.Examination_Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace ISas.Repository.ExaminationRepository.IRepository
{
    public interface IExamination_RemarksEntryRepo
    {
        List<SelectListItem> Get_RemarksEntryDropDowns(string SessionId, string UserId, string ClassId, string Mode, string ExamId);
        Examination_RemarksEntryModels Get_RemarksEntryStudentDetails(string SessionId, string UserId, string ClassId, string SectionId, string ExamTempleteId);
        Tuple<int, string> Examination_RemarksEntry_CRUD(DataTable dt, Examination_RemarksEntryModels model);
    }
}
