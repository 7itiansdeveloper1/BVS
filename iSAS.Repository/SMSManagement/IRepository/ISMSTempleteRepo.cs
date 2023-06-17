using ISas.Entities.SMSManagement;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.SMSManagement.IRepository
{
    public interface ISMSTempleteRepo
    {
        List<SelectListItem> GetTempleteTypeList();
        IEnumerable<SMSTempleteModel> GetAllSMSTempleteList(string SMSID = "");
        SMSTempleteModel GetSMSTempleteBySMSID(string SMSID);
        Tuple<int, string> SMSTemplete_CRUD(SMSTempleteModel model);

        List<UnDeliveredSMSModel> GetUnDeliveredSMSList(string UserId);

        List<UnDeliveredSMSDetailsModel> GetUnDeliveredSMSDetailList_ForTeacher(string MessegeId, string UserId);
        List<UnDeliveredSMSDetailsModel> GetUnDeliveredSMSDetailList_ForDropMessegePage(string ClassIds, string MobileNos);

        string SMS_DropMessages_CRUD(SMS_DropMessages_CRUDModel model);
        Tuple<List<SelectListItem>, List<SelectListItem>> GetSMS_DropMessagesDropDowns();


    }
}
