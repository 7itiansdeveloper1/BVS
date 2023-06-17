using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.CommonEntities
{
    public class Common_AttachemntRefrence
    {
        public string uniqueKey { get; set; }
        public string refrenceId { get; set; }
        public string docType { get; set; }
        public string filePath { get; set; }
        public string fileName { get; set; }
        public string remarks { get; set; }
        public string formName { get; set; }
        public string transKey { get; set; }

        public string updatedBy { get; set; }
        public string updatedDate { get; set; }
    }
}
