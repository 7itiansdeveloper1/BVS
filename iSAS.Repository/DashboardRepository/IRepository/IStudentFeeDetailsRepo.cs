using ISas.Entities.DashboardEntities;
using System.Collections.Generic;
using System.Data;

namespace ISas.Repository.DashboardRepository.IRepository
{
    public interface IStudentFeeDetailsRepo
    {
        //List<StudentFeeStatusModel> GetFeeStatusDetailsList(string UserID, int SessionID);
        StudentFeeStatusModel GetFeeStatusDetailsList(string UserID, int SessionID);
        List<StudentFeeStatusModel> GetFeeStatusDetails_TransportList(string UserID, int SessionID);
        List<StudentFeeDetailsModel> GetFeeDetailsListByDueDate(string UserID, string DueDate, string FeeMode, int SessionID = 4);
        DataSet GetFeeDetails_ForReport(string UserID, string TransRefNo, string Mode, int SessionID);

        //List<StudentFeeStatusModel> GetFeeStatusDetailsList_StudDash(string UserID, int SessionID);
        StudentFeeDashbaord GetFeeStatusDetailsList_StudDash(string UserID, int SessionID);
        StudentFeeDashbaord GetFeeStatusDetailsList_StudDash(string UserID, int SessionID, string studentImage);

        StudentLedgerDetailsModel GetFeeStatusDetailsList_StudDash1(string UserID, int SessionID);
        FeeBillingInfoModel GetFeeBillingInfo(string ERPNo, string DueDate, string SessionID, string userid);
        DataSet NoDuesRecept(string erpNo, string sessionId);
        DataSet GetFeeDetails_ForStudentCopy(string UserID, string TransRefNo, int SessionID);
        DataSet GetFeeInvoiceDetails_ForReport(string erpNo, string sessionId, string duedate);


    }
}
