using ISas.Entities.FeesEntities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_ReceiptHeaderMasterRepo
    {
        List<Fee_ReceiptHeaderMasterModels> GetFee_ReceiptHeaderMasterList(string HeaderId);
        Fee_ReceiptHeaderMasterModels Fee_ReceiptHeaderMasterById(string HeaderId);
        Tuple<int, string> Fee_ReceiptHeaderMaster_CRUD(Fee_ReceiptHeaderMasterModels model);
        Tuple<int, string> Fee_ReceiptHeaderMaster_CRUD(string HeaderId);

        List<Fee_WingHeaderSetupModel> WingHeaderSetupDetailsList(string WingId);
        Fee_WingHeaderSetupModel WingHeaderSetupDetails(string WingId);
        Tuple<int, string> Fee_WingHeaderSetup_CRUD(Fee_WingHeaderSetupModel model);
        Tuple<int, string> Fee_WingHeaderSetup_CRUD(string WingId);
    }
}
