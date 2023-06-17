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
    public class Academic_QualificationMasterController : Controller
    {
        private IAcademic_QualificationMasterRepo _qualiMstRepo;
        public Academic_QualificationMasterController(IAcademic_QualificationMasterRepo qualiMstRepo)
        {
            _qualiMstRepo = qualiMstRepo;
        }

        // GET: Fee_FeeHeadMaster
        public ActionResult LandingPage()
        {
            return PartialView(_qualiMstRepo.GetQualificationList());
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Academic_QualificationMaster_CRUD(Academic_QualificationMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _qualiMstRepo.Academic_QualificationMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_QualificationMaster_Delete(string QualifId)
        {
            Tuple<int, string> res = _qualiMstRepo.Academic_QualificationMaster_CRUD(QualifId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }


    }
}