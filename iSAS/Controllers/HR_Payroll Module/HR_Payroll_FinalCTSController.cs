using ISas.Entities.HR_Payroll_Entities;
using ISas.Repository.HR_PayrollRepo.IRepository;
using ISas.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.HR_Payroll_Module
{
    [Authorize]
    [ExceptionHandler]
    public class HR_Payroll_FinalCTSController : Controller
    {
        private IHR_Payroll_FinalCTSRepo _fincalCTSRepo;

        public HR_Payroll_FinalCTSController(IHR_Payroll_FinalCTSRepo fincalCTSRepo)
        {
            _fincalCTSRepo = fincalCTSRepo;
        }


        [EncryptedActionParameter]
        public ActionResult LandingPage(string PayBandId, string PayBandName)
        {
            return View(_fincalCTSRepo.GetFinalCTSDetails(PayBandId, PayBandName));
        }


        [HttpPost]
        public JsonResult HR_Payroll_FinalCTS_CRUD(HR_Payroll_FinalCTSModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                string messege = "";
                messege = _fincalCTSRepo.HR_Payroll_FinalCTS_CRUD(model);

                return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}