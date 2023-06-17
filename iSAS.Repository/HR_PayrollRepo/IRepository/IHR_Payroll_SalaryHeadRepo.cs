using ISas.Entities.HR_Payroll_Entities;
using System;
using System.Collections.Generic;


namespace ISas.Repository.HR_PayrollRepo.IRepository
{
    public interface IHR_Payroll_SalaryHeadRepo
    {
        List<HR_Payroll_SalaryHeadModels> GetSalaryHeadList(string HeadID);
        HR_Payroll_SalaryHeadModels GetSalaryHeadByID(string HeadID);
        Tuple<int, string> HR_Payroll_SalaryHead_CRUD(HR_Payroll_SalaryHeadModels model);
        Tuple<int, string> HR_Payroll_SalaryHead_CRUD(string HeadID);
    }
}
