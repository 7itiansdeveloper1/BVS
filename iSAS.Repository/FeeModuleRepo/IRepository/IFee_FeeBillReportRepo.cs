using ISas.Entities.FeesEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_FeeBillReportRepo
    {
        Fee_FeeBillReportHtmlModel Fee_BillReportDetails(Fee_FeeBillReportModels param);
    }
}
