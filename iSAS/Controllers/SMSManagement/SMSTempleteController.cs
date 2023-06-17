using ISas.Entities.SMSManagement;
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
    public class SMSTempleteController : Controller
    {
        private ISMSTempleteRepo _smsTempleteRepo;
        public SMSTempleteController(ISMSTempleteRepo smsTemplete)
        {
            this._smsTempleteRepo = smsTemplete;
        }

        public ActionResult SMS_TemplateLandingPage()
        {
            return View(this._smsTempleteRepo.GetAllSMSTempleteList());
        }

        public ActionResult NewTempleteSMS()
        {
            SMSTempleteModel model = new SMSTempleteModel();
            model.Function = "SAVE";

            model.TempleteTypeList = this._smsTempleteRepo.GetTempleteTypeList();
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult UpdateTempleteSMS(string SMSID)
        {
            SMSTempleteModel model = this._smsTempleteRepo.GetSMSTempleteBySMSID(SMSID);
            model.Function = "UPDATE";

            model.TempleteTypeList = this._smsTempleteRepo.GetTempleteTypeList();
            return View(model);
        }

        [HttpPost]
        public JsonResult TempleteSMS_CRUD(SMSTempleteModel model)
        {
            model.CreatedBy = Session["UserID"].ToString();

            if (ModelState.IsValid)
            {
                Tuple<int, string> res = _smsTempleteRepo.SMSTemplete_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }

            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult SMS_TeacherPage()
        {
            TeacherSMSPageModel model = new TeacherSMSPageModel();
            model.UnDeliveredSMSList = this._smsTempleteRepo.GetUnDeliveredSMSList(Session["UserID"].ToString());
            return View(model);
        }

        public ActionResult _UnDeliveredSMSDetails_ForTeacher(string MessegeId, string IsForPrint = "NO")
        {
            List<UnDeliveredSMSDetailsModel> undeliveredSMSDetailList = this._smsTempleteRepo.GetUnDeliveredSMSDetailList_ForTeacher(MessegeId, Session["UserID"].ToString());
            //ViewBag.IsForPrint = "NO";
            //return PartialView("_UnDeliveredSMSDetails", undeliveredSMSDetailList);
            if (IsForPrint == "YES")
            {
                undeliveredSMSDetailList = new List<UnDeliveredSMSDetailsModel>();
                if (Session["UndeliveredSMSListForPrint_Teacher"] != null)
                    undeliveredSMSDetailList = Session["UndeliveredSMSListForPrint_Teacher"] as List<UnDeliveredSMSDetailsModel>;

                ViewBag.IsForPrint = IsForPrint;
                return View("_UnDeliveredSMSDetails", undeliveredSMSDetailList);
            }
            else
            {
                Session["UndeliveredSMSListForPrint_Teacher"] = undeliveredSMSDetailList;
                ViewBag.IsForPrint = IsForPrint;
                return PartialView("_UnDeliveredSMSDetails", undeliveredSMSDetailList);
            }
        }

        public ActionResult _UnDeliveredSMSDetails()
        {
            return View();
        }

        public ActionResult SMS_DropMessages()
        {
            SMS_DropMessagesPageModel model = new SMS_DropMessagesPageModel();
            Tuple<List<SelectListItem>, List<SelectListItem>> dropDownList = this._smsTempleteRepo.GetSMS_DropMessagesDropDowns();
            model.ClassList = dropDownList.Item1;
            model.TempleteSMSList = dropDownList.Item2;
            return View(model);
        }

        public ActionResult _UnDeliveredSMSDetails_ForDropMessegePage(string ClassIds, string MobileNos, string IsForPrint = "NO")
        {
            List<UnDeliveredSMSDetailsModel> undeliveredSMSDetailList = this._smsTempleteRepo.GetUnDeliveredSMSDetailList_ForDropMessegePage(ClassIds, MobileNos);
            if(IsForPrint == "YES")
            {
                undeliveredSMSDetailList = new List<UnDeliveredSMSDetailsModel>();
                if(Session["UndeliveredSMSListForPrint"] != null )
                    undeliveredSMSDetailList = Session["UndeliveredSMSListForPrint"] as List<UnDeliveredSMSDetailsModel>;

                ViewBag.IsForPrint = IsForPrint;
                return View("_UnDeliveredSMSDetails", undeliveredSMSDetailList);
            }
            else
            {
                Session["UndeliveredSMSListForPrint"] = undeliveredSMSDetailList;
                ViewBag.IsForPrint = IsForPrint;
                return PartialView("_UnDeliveredSMSDetails", undeliveredSMSDetailList);
            }
        }


        //
        [HttpPost]
        public JsonResult SMS_DropMessages_CRUD(SMS_DropMessages_CRUDModel model)
        {
            model.UserId = Session["UserID"].ToString();

            if (ModelState.IsValid)
            {
                string messege = this._smsTempleteRepo.SMS_DropMessages_CRUD(model);
                return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = "failed", Msg = "There is an error on save try again after some time ..!" }, JsonRequestBehavior.AllowGet);
        }
    }
}
