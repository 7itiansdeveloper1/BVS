using ISas.Entities.HR_Payroll_Entities;
using ISas.Repository.HR_PayrollRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.HR_Payroll_Module
{
    [Authorize]
    [ExceptionHandler]
    public class HR_Payroll_LeaveMasterController : Controller
    {
        private IHR_Payroll_LeaveMasterRepo _leaveMstRepo;
        public HR_Payroll_LeaveMasterController(IHR_Payroll_LeaveMasterRepo leaveMst)
        {
            _leaveMstRepo = leaveMst;
        }

        public PartialViewResult LandingPage()
        {
            return PartialView(_leaveMstRepo.GetLeaveMasterList(""));
        }

        public ActionResult New()
        {
            return View();
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string LvID)
        {
            return View(_leaveMstRepo.GetLeaveMasterByID(LvID));
        }

        [HttpPost]
        public JsonResult HR_Payroll_LeaveMaster_CRUD(HR_Payroll_LeaveMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _leaveMstRepo.HR_Payroll_LeaveMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HR_Payroll_LeaveMaster_Delete(string LvID)
        {
            Tuple<int, string> res = _leaveMstRepo.HR_Payroll_LeaveMaster_CRUD(LvID);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}