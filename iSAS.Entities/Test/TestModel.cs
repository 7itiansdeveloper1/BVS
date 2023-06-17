using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.Test
{
    public class TestModel
    {
        public TestModel()
        {
            //statusList = new List<SelectListItem>();
            //onlineTransctionsList = new List<onlineTransctions>();
        }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        [Required(ErrorMessage = "Date is required..!")]
        [Display(Name = "Date")]
        public string rectiptDate { get; set; }
        [Display(Name = "Status")]
        public string selectedStatusId { get; set; }

        public List<SelectListItem> statusList = new List<SelectListItem>()
            {
                new SelectListItem() {Text="Pending", Value="P"},
                new SelectListItem() {Text="Failed", Value="F"},
                new SelectListItem() {Text="Unsuccessful", Value="UN"},
                new SelectListItem() {Text="Success", Value="S"},
            };

        public List<onlineTransctions> onlineTransctionsList = new List<onlineTransctions>()
        {
               new onlineTransctions() { Sno=1,IsSelected=false,IsEditable=true,Student="Mahesh Kumar",father="Tilak Ram",className="I-A",Installment="DEC-22"
               ,amount=4650,bankReferenceNo="235177017845",orderId="123456",paymentMode="Credit Card",bankTrackingNo="123",paymentDate="17/12/2022 11:26",paymentStatus="SUCCESS"
               },
               new onlineTransctions() { Sno=2,IsSelected=false,IsEditable=true,Student="Mahesh Kumar1",father="Tilak Ram1",className="I-B",Installment="DEC-22"
               ,amount=4650,bankReferenceNo="235177017846",orderId="123457",paymentMode="Credit Card",bankTrackingNo="1234",paymentDate="17/12/2022 11:28",paymentStatus="SUCCESS"
               }
        };
    }

    public class onlineTransctions
    {
        public int Sno { get; set; }
        public bool IsEditable { get; set; }
        public bool IsSelected { get; set; }
        public string Student { get; set; }
        public string father { get; set; }
        public string className { get; set; }
        public string Installment { get; set; }
        public int amount { get; set; }
        public string bankReferenceNo { get; set; }
        public string orderId { get; set; }
        public string paymentMode { get; set; }
        public string bankTrackingNo { get; set; }
        public string paymentDate { get; set; }
        public string paymentStatus { get; set; }

    }


}

