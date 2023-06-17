using ISas.Entities.HR_Payroll_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.HR_PayrollRepo.IRepository
{
    public interface IHR_Payroll_CTC_SetupRepo 
    {
        List<HR_Payroll_CTC_SetupModels> GetCTCList(string PayBandID);
        Tuple<int, string> HR_Payroll_CTC_CRUD(HR_Payroll_CTC_SetupModels model);
    }
}
