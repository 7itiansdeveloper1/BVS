using ISas.Entities.HR_Payroll_Entities;
using System;
using System.Collections.Generic;


namespace ISas.Repository.HR_PayrollRepo.IRepository
{
    public interface IHR_Payroll_PayBandMasterRepo
    {
        List<HR_Payroll_PayBandMasterModels> GetPayBandList(string PayBandID);
        HR_Payroll_PayBandMasterModels GetPayBandByID(string PayBandID);
        Tuple<int, string> HR_Payroll_PayBandMaster_CRUD(HR_Payroll_PayBandMasterModels model);
        Tuple<int, string> HR_Payroll_PayBandMaster_CRUD(string PayBandID);
    }
}
