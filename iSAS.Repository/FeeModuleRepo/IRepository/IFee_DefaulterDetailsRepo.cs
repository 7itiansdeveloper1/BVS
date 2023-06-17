using ISas.Entities.FeesEntities;
using System.Data;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_DefaulterDetailsRepo
    {
        Fee_FilterDefaulterDetailModel GetDefaulterDetails(string SessionId, string DueDate, string ClassId, string FeeCategoryId, string DefaulterType, string ReportType); //string SectionId, 
        Fee_FilterDefaulterDetailModel GetDefaulterDetails1(string SessionId, string DueDate, string ClassId, string FeeCategoryId, string DefaulterType, bool IncludeInactive, bool IncludeNonAdmitted, bool IncludePaid);
        DataSet GetDefaulterLetterDetails(Fee_FilterDefaulterDetailModel model);
        string GetSMSTextForFee();
    }
}
