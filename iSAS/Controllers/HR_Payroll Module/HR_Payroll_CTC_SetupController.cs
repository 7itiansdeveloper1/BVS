using ISas.Entities.HR_Payroll_Entities;
using ISas.Repository.HR_PayrollRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.HR_Payroll_Module
{
    [Authorize]
    [ExceptionHandler]
    public class HR_Payroll_CTC_SetupController : Controller
    {
        private IHR_Payroll_CTC_SetupRepo _ctcSetupRepo;
        private IHR_Payroll_SalaryHeadRepo _salaryHeasMstRepo;
        public HR_Payroll_CTC_SetupController(IHR_Payroll_CTC_SetupRepo ctcSetupRepo, IHR_Payroll_SalaryHeadRepo salaryHeadRepo)
        {
            _ctcSetupRepo = ctcSetupRepo;
            _salaryHeasMstRepo = salaryHeadRepo;
        }

        public PartialViewResult LandingPage(string PayBandID)
        {
            return PartialView(_ctcSetupRepo.GetCTCList(PayBandID));
        }


        [EncryptedActionParameter]
        public ActionResult New(string PayBandID, string PayBandName)
        {
            HR_Payroll_CTC_SetupModels model = new HR_Payroll_CTC_SetupModels();
            model.HeadList = _salaryHeasMstRepo.GetSalaryHeadList("").Select(r => new SelectListItem
            {
                Text = r.HeadName,
                Value = r.HeadID
            }).ToList();

            model.PayBandId = PayBandID; model.PayBandName = PayBandName;
            for (int i = 0; i < 5; i++)
            {
                model.SlabList.Add(new SlabDetailsModels { });
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult HR_Payroll_CTC_Setup_CRUD(HR_Payroll_CTC_SetupModels model)
        {
            for (int i = 0; i < model.SlabList.Count; i++)
            {
                if (!string.IsNullOrEmpty(model.SlabList[i].Minimum) || !string.IsNullOrEmpty(model.SlabList[i].Maximum)
                    || !string.IsNullOrEmpty(model.SlabList[i].Result))
                {
                    if (string.IsNullOrEmpty(model.SlabList[i].Minimum) || string.IsNullOrEmpty(model.SlabList[i].Maximum)
                   || string.IsNullOrEmpty(model.SlabList[i].Result))
                    {
                        ModelState.AddModelError("SlabValidationMsg", "All value of row in slab is req..!");
                    }
                }
            }

            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _ctcSetupRepo.HR_Payroll_CTC_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult HR_Payroll_CTC_Setup_CRUD_Delete(string HeadID)
        {
            string messege = ""; // _salaryHeasMstRepo.HR_Payroll_SalaryHead_CRUD(HeadID);
            return Json(new { status = "success", Msg = messege , Color =  "Success"}, JsonRequestBehavior.AllowGet);
        }
    }
}