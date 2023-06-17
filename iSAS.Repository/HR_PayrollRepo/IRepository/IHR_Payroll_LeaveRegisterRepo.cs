using ISas.Entities.HR_Payroll_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.HR_PayrollRepo.IRepository
{
    public interface IHR_Payroll_LeaveRegisterRepo
    {
        HR_Payroll_LeaveRegisterModels GetFormLoadDetails();
        HR_Payroll_LeaveRegisterModels GetLocalLeaveDetails(string EmpID);
        HR_Payroll_LeaveRegisterModels GetLeaveDetails(string EmpID);

        Tuple<int, string> HR_Payroll_LeaveRegister_CRUD(HR_Payroll_LeaveRegisterModels model);

        int GetLeaveAnnualQuota(string LeaveID);
    }
}
