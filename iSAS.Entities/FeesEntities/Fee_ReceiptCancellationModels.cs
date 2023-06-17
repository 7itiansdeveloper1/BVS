using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.FeesEntities
{
    public class Fee_ReceiptCancellationModels
    {
        public Fee_ReceiptCancellationModels()
        {
            ReceiptDetail_OfDishList = new List<ReceiptDetailModel>();
        }

        [Display(Name ="Ref. No")]
        public string TransRefNo { get; set; }
        public string Mode { get; set; }

        [Display(Name = "Bank")]
        public string TransBank { get; set; }

        [Display(Name = "Branch")]
        public string TransBranch { get; set; }

        [Display(Name ="Reference No.")]
        public string TransReferenceNo { get; set; }

        [Display(Name = "Date")]
        public string TransDate { get; set; }

        [Display(Name = "Dishonour Date")]
        public string DishDate { get; set; }

        [Display(Name = "Dishonour Amount")]
        public int DishAmt { get; set; }

        //Extra Feild

        [Display(Name = "Amount")]
        public int ChequeAmt { get; set; }
        public string CRUDMode { get; set; }

        public string Selected_ERPNo { get; set; }
        public string Selected_ReceiptNo { get; set; }
        public string SessionId { get; set; }
        public string UserId { get; set; }
        public string Selected_StudentName { get; set; }

        public List<ReceiptDetailModel> ReceiptDetail_OfDishList { get; set; }
    }


}
