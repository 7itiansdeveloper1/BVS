using ISas.Entities.FeesEntities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_FinePolicyRepo
    {
        List<Fee_FinePolicyModels> GetFinePolicyList(string StructId, string StructName, int PolicyId);
        Fee_FinePolicyModels GetFinePolicyById(string StructId, string StructName, int PolicyId);

        Tuple<int, string> Fee_FinePolicy_CRUD(Fee_FinePolicyModels model);
        Tuple<int, string> Fee_FinePolicy_CRUD(int PolicyId);
    }
}
