using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_InstallmentSetupController : Controller
    {
        private IFee_InstallmentSetupRepo _installmetRepo;
        public Fee_InstallmentSetupController(IFee_InstallmentSetupRepo installmentRepo)
        {
            _installmetRepo = installmentRepo;
        }

        // GET: Fee_FeeHeadMaster
        //[EncryptedActionParameter]
        public ActionResult LandingPage(string StructId)
        {
            return PartialView(_installmetRepo.GetInstallmetsByStructId(StructId, "", Session["SessionId"].ToString()));
        }

        //[EncryptedActionParameter]
        [CustomAuthorizeFilter("NEW_WITH_VIEW")]
        public ActionResult New(string StructId, string StrectureName)
        {
            Fee_InstallmentSetupModels model = new Fee_InstallmentSetupModels();
            model.StrectureName = StrectureName;
            model.StructId = StructId;
            model.Active = true;
            return View(model);
        }

        [CustomAuthorizeFilter("UPDATE")]
        [EncryptedActionParameter]
        public ActionResult Updation(string StructId, string InstallId)
        {
            Fee_InstallmentSetupModels model = _installmetRepo.GetInstallmetsByStructId(StructId, InstallId, Session["SessionId"].ToString()).FirstOrDefault();
            return View(model);
        }

        [HttpPost]
        public JsonResult Fee_InstallmentSetup_CRUD(Fee_InstallmentSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                model.SessionId = Session["SessionId"].ToString();
                Tuple<int, string> res = _installmetRepo.Fee_InstallmentSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeFilter("DELETE")]
        public JsonResult Fee_InstallmentSetup_Delete(string InstallId)
        {
            Tuple<int, string> res = _installmetRepo.Fee_InstallmentSetup_CRUD(InstallId, Session["UserId"].ToString());
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult InstallmetDropdwon_ByStrectureId(string StrectureId)
        {
            return Json(_installmetRepo.InstallmetDropdwon_ByStrectureId(StrectureId), JsonRequestBehavior.AllowGet);
        }
    }
}