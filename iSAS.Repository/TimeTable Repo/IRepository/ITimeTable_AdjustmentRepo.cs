using ISas.Entities.TimeTable_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.TimeTable_Repo.IRepository
{
    public interface ITimeTable_AdjustmentRepo
    {
        TimeTable_Adjustment_FormLoadModel TimeTable_Adjustment_FormLoad(string Date);
        List<SelectListItem> GetEffectedClassList(string Date, string TeacherId);
        TimeTable_Adjustment_TransactionModel GetEffectedPeriodWithAvailableStaff(string TeacherId, string Date, string ClassSecId);
        Tuple<int, string> TimeTable_Adjustment_CRUD(TimeTable_Adjustment_TransactionModel model);
        TimeTable_Adjustment_FormLoadModel DayAdjustment(string AdjustmentDate, string wingid,string reportName);
        TimeTable_Adjustment_FormLoadModel TimeTable_Adjustment_FormLoad();

    }
}
