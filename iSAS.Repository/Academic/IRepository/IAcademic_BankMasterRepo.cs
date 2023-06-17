using ISas.Entities.Academic;
using System;
using System.Collections.Generic;

namespace ISas.Repository.Academic.IRepository
{
    public interface IAcademic_BankMasterRepo
    {
        List<Academic_BankMasterModels> GetBankList();

        Tuple<int, string> Academic_BankMaster_CRUD(Academic_BankMasterModels model);
        Tuple<int, string> Academic_BankMaster_CRUD(string BankId);
    }
}
