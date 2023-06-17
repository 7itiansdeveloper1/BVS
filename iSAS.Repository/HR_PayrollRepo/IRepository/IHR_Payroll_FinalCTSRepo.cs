using ISas.Entities.HR_Payroll_Entities;


namespace ISas.Repository.HR_PayrollRepo.IRepository
{
    public interface IHR_Payroll_FinalCTSRepo
    {
        HR_Payroll_FinalCTSModels GetFinalCTSDetails(string PayBandID, string PayBandName);

        string HR_Payroll_FinalCTS_CRUD(HR_Payroll_FinalCTSModels model);
    }
}
