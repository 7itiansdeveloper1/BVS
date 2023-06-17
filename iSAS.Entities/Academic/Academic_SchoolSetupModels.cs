using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.Academic
{
    public class Academic_SchoolSetupModels
    {
        public int SchoolId { get; set; }

        [StringLength(200, ErrorMessage ="Only 200 Char. is Allowed..!")]
        [Display(Name ="School Name")]
        [Required(ErrorMessage ="School is Req..!")]
        public string ClientName { get; set; }

        [Display(Name = "Alias")]
        public string Alias { get; set; }

        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required(ErrorMessage ="Address is Req..!")]
        [Display(Name = "Address")]
        public string Add { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Pin Code")]
        public string Pincode { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Phone No.")]
        public string Tel { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }


        //[DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Invalid EmailId..!")]
        public string Email { get; set; }

        [Display(Name = "Website")]
        public string Web { get; set; }

        [Display(Name = "Header 1")]
        public string Header1 { get; set; }

        [Display(Name = "Header 2")]
        public string Header2 { get; set; }

        [Display(Name = "Header 3")]
        public string Header3 { get; set; }

        [Display(Name = "Affiliation Code")]
        public string AffiliationCode { get; set; }

        [Display(Name = "Affiliation Board")]
        public string AffiliationBoard { get; set; }

        [Display(Name = "Affiliation Year")]
        public string AffiliationYear { get; set; }


        public bool IsDeletable { get; set; }

        [Display(Name ="Upload Logo")]
        public string Logo { get; set; }

        //Extra Feild
        public string CRUDMode { get; set; }
    }
}
