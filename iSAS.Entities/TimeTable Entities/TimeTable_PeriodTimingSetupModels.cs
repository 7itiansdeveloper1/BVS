using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.TimeTable_Entities
{
    public class TimeTable_PeriodTimingSetupModels
    {
        public TimeTable_PeriodTimingSetupModels()
        {
            SeasonList = new List<SelectListItem>();
            PeriodTimingList = new List<PeriodTimingDetailModels>();
            ClassList = new List<SelectListItem>();
        }

        public string ClassSectionId { get; set; }
        public string ClassSectionName { get; set; }
        public string ClassTeacherName { get; set; }

        public string SeasonId { get; set; }
        public string UserId { get; set; }
        public List<SelectListItem> SeasonList { get; set; }
        public List<SelectListItem> ClassList { get; set; }
        public List<PeriodTimingDetailModels> PeriodTimingList { get; set; }
    }

    public class PeriodTimingDetailModels
    {
        public string PeriodName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}
