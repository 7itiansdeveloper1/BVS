using ISas.Entities.HR_Payroll_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Repository.HR_PayrollRepo.IRepository
{
    public interface IHR_Payroll_AttendanceProcessRepo
    {
        HR_Payroll_AttendanceProcessModels GetAttenProcessFormLoadDetails(string SessionId);
        HR_Payroll_AttendanceProcessModels GetMonthleyAttenList(string PayBandId, string MonthDate);
    }
}
