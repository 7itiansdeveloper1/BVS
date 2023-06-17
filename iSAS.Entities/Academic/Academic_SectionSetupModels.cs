using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.Academic
{
    public class Academic_SectionSetupModels
    {
        public Academic_SectionSetupModels()
        {
            SectionList = new List<SelectListItem>();
            TeacherList = new List<SelectListItem>();
        }
        public string ClassId { get; set; }

        [Required(ErrorMessage = "Section is Req..!")]
        [Display(Name = "Section")]
        public string SectionId { get; set; }


        public string Class { get; set; }

        [Range(1, 500, ErrorMessage = "Must be between 1 to max limit..!")]
        [Display(Name = "Max. Strength")]
        public int MaxStrength { get; set; }


        public int CurrentStrength { get; set; }
        public bool IsDeletable { get; set; }

        [Display(Name = "Class Teacher 1")]
        public string CT1 { get; set; }

        [Display(Name = "Class Teacher 2")]
        public string CT2 { get; set; }

        public string ClassTeacher { get; set; }

        //Extra Feild
        public int Class_Strength { get; set; }
        public string Class_Name { get; set; }
        public string Class_ClassId { get; set; }

        public string UserId { get; set; }
        public string CRUDMode { get; set; }

        public string WingName { get; set; }
        public string WingId { get; set; }
        public string SchoolId { get; set; }
        public string SchoolName { get; set; }


        public List<SelectListItem> SectionList { get; set; }
        public List<SelectListItem> TeacherList { get; set; }

    }
}
