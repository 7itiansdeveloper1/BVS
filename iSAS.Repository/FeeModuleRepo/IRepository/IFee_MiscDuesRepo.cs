using ISas.Entities.FeesEntities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ISas.Repository.FeeModuleRepo.IRepository
{
    public interface IFee_MiscDuesRepo
    {
        Fee_MiscDueModel GetMist_FromLoadDropDownList(string SessionId, string UserId);
        List<SelectListItem> getSectionList(string SessionId, string UserId, string ClassId, string FeeHeadId);
        List<SelectListItem> getStructureList(string SessionId, string UserId, string ClassId, string SectionId, string FeeHeadId);
        List<SelectListItem> getInstallmentList(string SessionId, string UserId, string ClassId, string SectionId, string StrectureId, string FeeHeadId);
        List<Fee_MiscDuesStudentDetailsModel> getStudentDetailsListList(string SessionId, string UserId, string ClassId, string SectionId, string StrectureId, string DueDate, string FeeHeadId);

        List<SelectListItem> GetMistHeadList();
        List<SelectListItem> GetMiscInstallmentList(string SessionId, string ERPNo);
        List<Fee_MiscDuesDetails> GetMiscDueList(string SessionId, string ERPNo);
        Tuple<int, string> Fee_MiscDues_CRUD(Fee_MiscDueModel model);


        /// <summary>
        /// Get Student Dues Head List
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="erpNo"></param>
        /// <returns></returns>
        List<SelectListItem> getStudentDueHeadList(string sessionId, string erpNo);

        /// <summary>
        /// Get Student Head Wise Dues
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="erpNo"></param>
        /// <param name="headId"></param>
        /// <returns></returns>
        List<Fee_StudentHeadWiseDueDetailsModel> getStudentDuesHeadWise(string sessionId, string erpNo, string headId);
        Tuple<int, string> Fee_MiscDues_StudentWise_CRUD(Fee_MiscDueModel model);
        Tuple<int, string> Fee_MiscDues_StudentWise_CRUD(string erpNo, string headId, string sessionId, string dueDate, string invRefNo,string userId);
    }
}
