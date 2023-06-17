using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.LibraryEntities
{
    public class Library_SupplierMasterModels
    {
        public Library_SupplierMasterModels()
        {
            CountryList = new List<SelectListItem>();
            StateList = new List<SelectListItem>();
            CityList = new List<SelectListItem>();
        }

        public string SupplierId { get; set; }

        [Required(ErrorMessage ="Supplier Name is Req..!")]
        [StringLength(100, ErrorMessage ="Max 100 char is Allowed..!")]
        [Display(Name ="Supplier Name")]
        public string SupplierName { get; set; }

        [Display(Name = "Address")]
        [StringLength(100, ErrorMessage = "Max 500 char is Allowed..!")]
        public string SupplierAdd { get; set; }

        [Display(Name = "City")]
        public string SupplierCity { get; set; }

        [Display(Name = "State")]
        public string SupplierState { get; set; }

        [Display(Name = "Country")]
        public string SupplierCountry { get; set; }

        [Display(Name = "Contact")]
        public string SupplierContact { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        public int PublishStrength { get; set; }
        public bool IsDeleteable { get; set; }

        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> CityList { get; set; }

        //Extra Feild
        public string UserId { get; set; }
        public string CRUDMode { get; set; }
    }
}
