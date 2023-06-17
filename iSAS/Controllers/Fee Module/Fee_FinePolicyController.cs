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
    public class Fee_FinePolicyController : Controller
    {
        private IFee_FinePolicyRepo _finePolicyRepo;
        public Fee_FinePolicyController(IFee_FinePolicyRepo finePolicyRepo)
        {
            _finePolicyRepo = finePolicyRepo;
        }

        // GET: Fee_FinePolicy
        public ActionResult LandingPage(string StructId, string StructName, int PolicyId = 0)
        {
            return PartialView(_finePolicyRepo.GetFinePolicyList(StructId, StructName, PolicyId));
        }

        [CustomAuthorizeFilter("NEW_WITH_VIEW")]
        [EncryptedActionParameter]
        public ActionResult New(string StructId, string StructName)
        {
            Fee_FinePolicyModels model = new Fee_FinePolicyModels();
            model.StructId = StructId;
            model.StructName = StructName;
            return View(model);
        }

        [CustomAuthorizeFilter("UPDATE")]
        [EncryptedActionParameter]
        public ActionResult Updation(string StructId, string StructName, string PolicyId)
        {
            return View(_finePolicyRepo.GetFinePolicyById(StructId, StructName, Convert.ToInt32(PolicyId)));
        }

        [HttpPost]
        public JsonResult Fee_FinePolicy_CRUD(Fee_FinePolicyModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _finePolicyRepo.Fee_FinePolicy_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }


        [CustomAuthorizeFilter("DELETE")]
        public JsonResult Fee_FinePolicy_Delete(int PolicyId)
        {
            Tuple<int, string> res = _finePolicyRepo.Fee_FinePolicy_CRUD(PolicyId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}