using ISas.Entities.TimeTable_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Repository.TimeTable_Repo.IRepository
{
    public interface ITimeTable_SetupRepo
    {
        List<TimeTable_ConfigurationModels> GetLandingPageDetails();
        TimeTable_SetupModels TimeTable_Setup_FormLoad_V1(string ClassSectionId);
        TimeTable_SetupModels TimeTable_Setup_FormLoad(string ClassSectionId);
        List<SelectListItem> GetTeacherListBySubjectId(string ClassSectionId, string SubjectId);
        Tuple<int, string> TimeTable_Setup_CRUD(TimeTable_SetupModels model);
        Tuple<int, string> TimeTable_Setup_CRUD_V1(TimeTable_SetupModels model);
        Tuple<int, string> TimeTable_Setup_CRUD_V2(string classsectionId, string p, string d, string pdValue, string userid, string mode);
        Tuple<bool, string> CheckStaffAvailabilityForTimeTableMapping(string StaffId, string PeriodName, int DayNo);
        TimeTable_SetupModels getSubjects(string p, string d, string ClassSectionId,string classsection, string classteacher);
    }
}
