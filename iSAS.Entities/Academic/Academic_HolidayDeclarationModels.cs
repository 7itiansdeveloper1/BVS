using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.Academic
{
    public class Academic_HolidayDeclarationModel
    {
        public string HolidayId { get; set; }

        [Display(Name ="From Date")]
        public string Fdate { get; set; }

        [Display(Name = "To Date")]
        public string TDate { get; set; }

        [Display(Name = "No of Days")]
        public int NoofDays { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }

        public string HolidayName { get; set; }
    }
}
