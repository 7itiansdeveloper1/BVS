using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.FeesEntities
{
    public   class Fee_PaymentGatewayMasterModel
    {

        public int gatewayId { get; set; }
        public string gatewayName { get; set; }
        public string merchantId { get; set; }
        public string merchant_KEY { get; set; }
        public string responseURL { get; set; }
        public string transactionURL { get; set; }
        public string statusAPIURL { get; set; }
        public bool IsActive { get; set; }
        public string moduleName { get; set; }
         
    }
}

