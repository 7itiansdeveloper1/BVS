using ISas.Entities.TimeTable_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.TimeTable_Repo.IRepository
{
    public interface ITimeTable_PeriodTimingSetupRepo
    {
        List<SelectListItem> GetSeasonList();
        TimeTable_PeriodTimingSetupModels GetPeriodTimingDetails(string ClassSectionId, string SeasonId);
        Tuple<int, string> TimeTable_PeriodTimingSetup_CRUD(TimeTable_PeriodTimingSetupModels model);
        Tuple<int, string> TimeTable_PeriodTimingSetup_CRUD(string SeasonId, string FromClassSecId, string ToClassSecId, string UserId);
    }
}
