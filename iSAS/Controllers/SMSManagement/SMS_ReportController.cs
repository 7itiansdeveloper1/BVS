using ISas.Entities.SMSManagement;
using ISas.Repository.Interface;
using ISas.Repository.SMSManagement.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.SMSManagement
{
    [Authorize]
    [ExceptionHandler]
    public class SMS_ReportController : Controller
    {
        private ISMS_ReportRepo _smsReportRepo;
        private ICommonRepo _commonRepo;
        public SMS_ReportController(ISMS_ReportRepo smsReportRepo, ICommonRepo commonRepo)
        {
            _smsReportRepo = smsReportRepo;
            _commonRepo = commonRepo;
        }

        // GET: SMS_Report
        public ActionResult SMS_ReportMain_Filter()
        {
            return View();
        }

        public PartialViewResult _SMS_ReportMain(string Date)
        {
            return PartialView(_smsReportRepo.GetSMSDeliveryReport("FormLoad", Date, Session["SessionId"].ToString()));
        }

        public PartialViewResult _MessegeDeliveryCount(string GID)
        {
            return PartialView(_smsReportRepo.GetSMSDeliveryCount(GID, Session["SessionId"].ToString()));
        }

        [EncryptedActionParameter]
        public ActionResult SMS_ReportDetails_Filter(string GID, string DStatus) //string GID, string DStatus
        {
            SMS_ReportDetailFilterModel model = new SMS_ReportDetailFilterModel();
            model.GID = GID;
            model.DStatus = "ALL";
            //model.DStatus = "ALL";
            return View(model);
        }

        public PartialViewResult _SMS_ReportDetails(string GID, string DStatus)
        {
            SMS_ReportDetailFilterModel model = new SMS_ReportDetailFilterModel();
            model.referenceList = _smsReportRepo.GetSMSDeliveryReportDetails("GIDDetail", GID, DStatus);
            model.GID = GID;
            //return PartialView(_smsReportRepo.GetSMSDeliveryReportDetails("GIDDetail", GID, DStatus));
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult ReSendSMS(SMS_ReportDetailFilterModel model)
        {
            //var referenceList = model as IEnumerable<SMS_ReportDetailModel>;
            List<SMS_ReportDetailModel> filteredReferenceList = model.referenceList.Where(r => r.IsSelected && !string.IsNullOrEmpty(r.MobileNo) && r.MobileNo.Length == 10).ToList();
            List<SMS_ReportDetailModel> duplicate = filteredReferenceList.Select((t) => new SMS_ReportDetailModel
            {
                IsSelected = t.IsSelected,
                ERPNo = t.ERPNo,
                GID = t.GID,
                MobileNo = t.MobileNo,
            }).GroupBy(g => g.MobileNo).Where(g => g.Count() > 1).SelectMany(r => r).ToList();

            filteredReferenceList = filteredReferenceList.DistinctBy(r => r.MobileNo).ToList();
            duplicate = duplicate.Where(r => !filteredReferenceList.Select(p => p.ERPNo).Contains(r.ERPNo)).ToList();
            Tuple<int, string> res = _commonRepo.ReSendSMS(model.GID, filteredReferenceList, duplicate);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
        //public PartialViewResult _SMS_ReportDetails_Print(string GID, string DStatus)
        //{
        //    return PartialView(_smsReportRepo.GetSMSDeliveryReportDetails("GIDDetail", GID, DStatus));
        //}
    }
}