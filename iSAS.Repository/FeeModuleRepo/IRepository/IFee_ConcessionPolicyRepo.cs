using ISas.Entities.FeesEntities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_ConcessionPolicyRepo
    {
        List<SelectListItem> GetUserRoleList();

        List<Fee_ConcessionPolicyModels> GetConcessionPolicyList(string ConcId);
        Fee_ConcessionPolicyModels GetConcessionPolicyById(string ConcId);

        string Fee_ConcessionPolicy_CRUD(Fee_ConcessionPolicyModels model);
        string Fee_ConcessionPolicy_CRUD(string ConcId);
    }
}
