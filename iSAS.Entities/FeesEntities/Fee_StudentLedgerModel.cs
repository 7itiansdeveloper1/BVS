using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ISas.Entities.FeesEntities
{
    public class Fee_StudentLedgerModel
    {

        [Display(Name = "Student")]
        public string Student { get; set; }
        [Display(Name = "Class & Section")]
        public string ClassName { get; set; }
        [Display(Name = "Adm No ")]
        public string AdmNo { get; set; }
        [Display(Name = "Father")]
        public string Father { get; set; }
        [Display(Name = "ERP")]
        public string ERP { get; set; }
        [Display(Name = "Due")]
        public int Due { get; set; }
        [Display(Name = "Concession")]
        public int Concession { get; set; }
        [Display(Name = "Disocunt")]
        public int discount { get; set; }
        [Display(Name = "Advance")]
        public int Advance { get; set; }
        [Display(Name = "Payable")]
        public int Payable { get; set; }

        [Display(Name = "Paid")]
        public int ActualPaid { get; set; }

        [Display(Name = "Balance")]
        public int Balance { get; set; }

        [Display(Name = "Fee Paid")]
        public int F { get; set; }

        [Display(Name = "Transport Paid")]
        public int T { get; set; }

        public List<Fee_StudentLedgerList> StudentLedgerList { get; set; }
        public List<Fee_StudentReceiptList> StudentReceiptList { get; set; }
        public List<Fee_InstallmentDetailsList> InstallmentDetailsList { get; set; }
    }
    public class Fee_StudentLedgerList
    {
        public Int64 Sno { get; set; }
        public string Duration { get; set; }
        public string DueDate { get; set; }
        public string ERP { get; set; }
        public int Due { get; set; }
        public int Concession { get; set; }
        public int discount { get; set; }
        public int advance { get; set; }
        public int Payable { get; set; }
        public int ActualPaid { get; set; }
        public int Balance { get; set; }
        public int PreviousBalance { get; set; }
        public string Status { get; set; }
    }
    public class Fee_StudentReceiptList
    {
        public Int64 Sno { get; set; }
        public string ERPNo { get; set; }
        public string ReceiptNo { get; set; }
        public string TransDate { get; set; }
        public string Duration { get; set; }
        public string Mode { get; set; }
        public int Due { get; set; } 
        public int Concession { get; set; }
        public int discount { get; set; }
        public int advance { get; set; }
        public int Payable { get; set; }
        public int Paid { get; set; }
        public int  Balance { get; set; }
        public int  RecAmount { get; set; }
        public int  Excess { get; set; }
        public string  Status { get; set; }

    }
    public class Fee_InstallmentDetailsList
    {
        public Int64 Sno { get; set; }
        public string Duration { get; set; }
        public string HeadName { get; set; }
        public int Due { get; set; }
        public int Concession { get; set; }
        public int discount { get; set; }
        public int advance { get; set; }
        public int Payable { get; set; }
        public int ActualPaid { get; set; }
        public int Balance { get; set; }
        public int PreviousBalance { get; set; }
    }
}
