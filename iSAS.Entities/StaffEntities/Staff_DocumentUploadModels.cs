using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.StaffEntities
{
    public class Staff_DocumentUploadModels
    {
        public Staff_DocumentUploadModels()
        {
            documentList = new List<SelectListItem>();
        }


        public string staffId { get; set; }
        public string DocId { get; set; }
        public string DocPath { get; set; }
        [Display(Name = "Alias(Name of Course)")]
        [Required(ErrorMessage = "Alias name is required..!")]
        public string docAlias { get; set; }

        public int docNo { get; set; }

        [Display(Name = "Certificate Date")]
        [Required(ErrorMessage ="Certification date is required..!")]
        public string certificateDate { get; set; }

        [Display(Name = "Training Done By")]
        public string TrainBy { get; set; }

        public List<SelectListItem> documentList { get; set; }
        public List<Staff_DocumentList> staffDocumentList { get; set; }
        public Staff_DocumentList uploadDocumentObject { get; set; }
    }


    public class Staff_DocumentList
    {
        public string staffId { get; set; }
        public string DocId { get; set; }
        public string DocName { get; set; }
        public string DocPath { get; set; }
        public string docAlias { get; set; }
        public string docfileName { get; set; }
        public string UploadedBy { get; set; }
        public string UplodedDate { get; set; }
        public int docNo { get; set; }
        public string certificateDate { get; set; }
        public string TrainBy { get; set; }
        public string TrainByName { get; set; }
    }

}
