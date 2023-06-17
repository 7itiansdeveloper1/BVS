using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_ConcessionPolicyController : Controller
    {
        private IFee_ConcessionPolicyRepo _feeConcessionRepo;

        public Fee_ConcessionPolicyController(IFee_ConcessionPolicyRepo feeConcessionRepo)
        {
            _feeConcessionRepo = feeConcessionRepo;
        }

        // GET: Fee_ConcessionPolicy
        [CustomAuthorizeFilter("VIEW")]
        public ActionResult LandingPage()
        {
            return View(_feeConcessionRepo.GetConcessionPolicyList(""));
        }

        [CustomAuthorizeFilter("NEW")]
        public ActionResult New()
        {
            Fee_ConcessionPolicyModels model = new Fee_ConcessionPolicyModels();
            model.UserRoleList = _feeConcessionRepo.GetUserRoleList();
            return View(model);
        }

        [CustomAuthorizeFilter("UPDATE")]
        [EncryptedActionParameter]
        public ActionResult Updation(string ConcId)
        {
            Fee_ConcessionPolicyModels model  =  _feeConcessionRepo.GetConcessionPolicyById(ConcId);
            model.UserRoleList = _feeConcessionRepo.GetUserRoleList();
            return View(model);
        }

        [HttpPost]
        public JsonResult Fee_ConcessionPolicy_CRUD(Fee_ConcessionPolicyModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                model.ConcCategory = "FEE";
                string messege = "";
                messege = _feeConcessionRepo.Fee_ConcessionPolicy_CRUD(model);

                return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorizeFilter("DELETE")]
        public JsonResult Fee_ConcessionPolicy_Delete(string ConcId)
        {
            string messege = _feeConcessionRepo.Fee_ConcessionPolicy_CRUD(ConcId);
            return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
        }

    }
}