using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.FeesEntities
{
    public class Fee_OnlineTransactionModel
    {

        public Fee_OnlineTransactionModel()
        {
            //statusList = new List<SelectListItem>();
            onlineTransctionsList = new List<onlineTransctions>();
        }


        public string fromDate { get; set; }
        public string toDate { get; set; }
        [Required(ErrorMessage = "Date is required..!")]
        [Display(Name = "Date")]
        public string rectiptDate { get; set; }
        [Display(Name = "Status")]
        public string selectedStatusId { get; set; }
        public List<SelectListItem> statusList = new List<SelectListItem>() {

                new SelectListItem() { Text = "Pending", Value = "P",Selected=true },
                new SelectListItem() { Text = "Failure", Value = "F" },
                new SelectListItem() { Text = "Unsuccessful", Value = "UN" },
                new SelectListItem() { Text = "Success", Value = "S" },
                new SelectListItem() { Text = "Receipt", Value = "R" }
        };

        public List<onlineTransctions> onlineTransctionsList  { get; set; }

}

    public class onlineTransctions
    {
        public bool isSelected { get; set; }
        public bool isEditable { get; set; }
        public bool isReadyForReceipt { get; set; }
        public string tnxId { get; set; }
        public string ERPNo { get; set; }
        public string bankRefNo { get; set; }
        public string trackingId { get; set; }
        public string statusMessage { get; set; }
        public int amount { get; set; }
        public string paymentMode { get; set; }
        public string paymentSuccessOn { get; set; }
        public string tnxBank { get; set; }
        public string DueDate { get; set; }
        public string InstallName { get; set; }
        public string Student { get; set; }
        public string Father { get; set; }
        public string SMSNo { get; set; }
        public string className { get; set; }
        public string Recpt { get; set; }
    }


}
