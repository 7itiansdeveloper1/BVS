using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_BankMasterController : Controller
    {
        private IAcademic_BankMasterRepo _bankMstRepo;
        public Academic_BankMasterController(IAcademic_BankMasterRepo bankMstRepo)
        {
            _bankMstRepo = bankMstRepo;
        }

        // GET: Fee_FeeHeadMaster
        public ActionResult LandingPage()
        {
            return PartialView(_bankMstRepo.GetBankList());
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Academic_BankMaster_CRUD(Academic_BankMasterModels model)
        {
            if (ModelState.IsValid)
            {
                //model.UserId = Session["UserId"].ToString();
                //model.CRUDMode = "SAVE";
                Tuple<int, string> res = _bankMstRepo.Academic_BankMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult Academic_BankMaster_Delete(string BankId)
        //{
        //    string messege = _bankMstRepo.Academic_BankMaster_CRUD(BankId);
        //    return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
        //}
    }
}