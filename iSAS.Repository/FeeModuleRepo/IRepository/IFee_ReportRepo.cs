using ISas.Entities.FeesEntities;
using System.Data;



namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_ReportRepo
    {
        Fee_ReportModels GetFee_Report_FormLoad(string ModuleName, string ReportName, string UserId, string sessionId);
        Fee_ReportModels GetStudentDetailReport(Fee_ReportModels model);
        DataSet GetFeeReport_Crystal(Fee_ReportModels model);
    }
}
