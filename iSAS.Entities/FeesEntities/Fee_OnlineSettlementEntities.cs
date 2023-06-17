using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.FeesEntities
{
    public class Fee_OnlineSettlementEntities
    {
        public Fee_OnlineSettlementEntities()
        {
            onlineSettlementList = new List<OnlineSettlementList>();
        }
        public List<OnlineSettlementList> onlineSettlementList { get; set; }
        public string dateRange { get; set; }
    }
    public class OnlineSettlementList
    {
        public bool isSettled { get; set; }
        public string ERPNo { get; set; }
        public string AdmNo { get; set; }
        public string Student { get; set; }
        public string Father { get; set; }
        public string Class { get; set; }
        public string TransRefNo { get; set; }
        public string Transdate { get; set; }
        public int recamount { get; set; }
        public string TransReferenceNo { get; set; }
        public string settlementDate { get; set; }
        
    }
}
