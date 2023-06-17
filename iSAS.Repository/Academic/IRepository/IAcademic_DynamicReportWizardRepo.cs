using ISas.Entities.Academic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_DynamicReportWizardRepo
    {
        List<Academic_DynamicReportWizardModels> GetReportWizardList();
        Academic_DynamicReportWizardModels NewReportData();
        List<SelectListItem> ReportFeildList(string ModuleId);
        Academic_DynamicReportWizardModels GetReportDetailsbyId(string ReportId, string ModuleId);
        Tuple<int, string> Academic_DynamicReportWizard_CRUD(Academic_DynamicReportWizardModels model);
    }
}
