using ISas.Entities.HR_Payroll_Entities;
using System;

namespace ISas.Repository.HR_PayrollRepo.IRepository
{
    public interface IHR_Payroll_SalaryRegisterRepo
    {
        HR_Payroll_SalaryRegisterModels GetSalaryRegisterFormLoadDetails(string SessionId);

        HR_Payroll_SalaryRegisterModels GetSalaryRegisterList(string PayBandId, string MonthDate);
        Tuple<int, string> HR_Payroll_SalaryRegister_CRUD(HR_Payroll_SalaryRegisterModels model);
    }
}
