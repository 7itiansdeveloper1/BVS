using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.FeesEntities
{
    public class Fee_FinePolicyModels
    {
        public Fee_FinePolicyModels()
        {
            FrequencyList = new List<SelectListItem>();
            TillDayList = new List<SelectListItem>();
            TillMonthList = new List<SelectListItem>();

            FrequencyList.Add(new SelectListItem { Text = "Per Day", Value = "D" });
            FrequencyList.Add(new SelectListItem { Text = "Per Month", Value = "M" });
            FrequencyList.Add(new SelectListItem { Text = "Fixed", Value = "F" });

            TillDayList.Add(new SelectListItem { Text = "5", Value = "5" });
            TillDayList.Add(new SelectListItem { Text = "10", Value = "10" });
            TillDayList.Add(new SelectListItem { Text = "15", Value = "15" });
            TillDayList.Add(new SelectListItem { Text = "20", Value = "20" });
            TillDayList.Add(new SelectListItem { Text = "25", Value = "25" });
            TillDayList.Add(new SelectListItem { Text = "Last Day of Month", Value = "LDM" });

            TillMonthList.Add(new SelectListItem { Text = "Same Month", Value ="SM" });
            TillMonthList.Add(new SelectListItem { Text = "Next Month", Value = "NM" });
            TillMonthList.Add(new SelectListItem { Text = "1", Value = "1" });
            TillMonthList.Add(new SelectListItem { Text = "2", Value = "2" });
            TillMonthList.Add(new SelectListItem { Text = "3", Value = "3" });
            TillMonthList.Add(new SelectListItem { Text = "Forever", Value = "FR" });
        }

        public int PolicyId { get; set; }
        public int Amount { get; set; }
        public string Frequency { get; set; }
        public string TillDay { get; set; }
        public string TillMonth { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDeletetable { get; set; }

        [Display(Name ="Fix Amount")]
        public int FixAmount { get; set; }

        public List<SelectListItem> FrequencyList { get; set; }
        public List<SelectListItem> TillDayList { get; set; }
        public List<SelectListItem> TillMonthList { get; set; }

        //Extra Feild
        public string StructId { get; set; }
        public string StructName { get; set; }
        public string UserId { get; set; }
        public string CRUDMode { get; set; }

        
    }
}
