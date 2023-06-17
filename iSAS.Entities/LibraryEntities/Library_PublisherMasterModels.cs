using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.LibraryEntities
{
    public class Library_PublisherMasterModels
    {
        public Library_PublisherMasterModels()
        {
            CountryList = new List<SelectListItem>();
            StateList = new List<SelectListItem>();
            CityList = new List<SelectListItem>();
        }

        public string PublisherId { get; set; }


        [Required(ErrorMessage ="Publisher Name is Req..!")]
        [StringLength(100, ErrorMessage ="Max 100 Char is Allowed..!")]
        [Display(Name ="Publisher Name")]
        public string PublisherName { get; set; }

        [StringLength(500, ErrorMessage = "Max 500 Char is Allowed..!")]
        [Display(Name = "Publisher Address")]
        public string PublisherAdd { get; set; }

        [Display(Name = "City")]
        public string PublisherCity { get; set; }

        [Display(Name = "State")]
        public string PublisherState { get; set; }

        [Display(Name = "Country")]
        public string PublisherCountry { get; set; }

        [Display(Name = "Contact")]
        public string PublisherContact { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }


        public int PublishStrength { get; set; }
        public bool IsDeleteable { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }

        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> CityList { get; set; }


    }
}
