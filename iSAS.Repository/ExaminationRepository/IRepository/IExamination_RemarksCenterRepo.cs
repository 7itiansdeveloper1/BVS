using ISas.Entities.Examination_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.ExaminationRepository.IRepository
{
    public interface IExamination_RemarksCenterRepo
    {
        List<Examination_RemarksTempleteModels> GetRemarksTempleteList(string TempleteId);
        Examination_RemarksTempleteModels GetRemarksTempleteById(string TempleId);
        Tuple<int, string> Examination_RemarksTemplete_CRUD(Examination_RemarksTempleteModels model);

        List<SelectListItem> GetRemarksTempleteClassList(string TempleteId);
        Tuple<int, string> RemarksTempleteClass_CRUD(Examination_TempleteClassSetupModel model);

        List<Examination_RemarksCenterModels> GetRemarksCenterList(string TempleteId, string RemarkId);
        Examination_RemarksCenterModels GetRemarksCenterById(string TempleId, string RemarkId);
        Tuple<int, string> Examination_RemarksCenter_CRUD(Examination_RemarksCenterModels model);
    }
}
