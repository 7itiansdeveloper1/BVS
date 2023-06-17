using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace ISas.Entities.Academic
{
    public class Academic_DocumentMappingModel
    {
        public Academic_DocumentMappingModel()
        {
            DepartmentList = new List<SelectListItem>();
            DocumentList = new List<DocumentListModel>();
        }
        [Display(Name = "Department")]
        public List<SelectListItem> DepartmentList { get; set; }
        public List<DocumentListModel> DocumentList { get; set; }
        public string DepartmentId { get; set; }

    }
    public class DocumentListModel
    {
        public bool Selected { get; set; }
        public string DocumentId { get; set; }
        public string DocumentName { get; set; }
    }
}
