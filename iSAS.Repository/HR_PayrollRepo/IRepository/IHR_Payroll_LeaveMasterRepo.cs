using ISas.Entities.HR_Payroll_Entities;
using System;
using System.Collections.Generic;

namespace ISas.Repository.HR_PayrollRepo.IRepository
{
    public interface IHR_Payroll_LeaveMasterRepo
    {
        List<HR_Payroll_LeaveMasterModels> GetLeaveMasterList(string LvID);
        HR_Payroll_LeaveMasterModels GetLeaveMasterByID(string LvID);
        Tuple<int, string> HR_Payroll_LeaveMaster_CRUD(HR_Payroll_LeaveMasterModels model);
        Tuple<int, string> HR_Payroll_LeaveMaster_CRUD(string LvID);
    }
}
