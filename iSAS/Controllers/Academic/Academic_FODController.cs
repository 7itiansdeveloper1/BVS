using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISas.Web.Models;
using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
namespace ISas.Web.Controllers.Academic
{

    [Authorize]
    [ExceptionHandler]
    public class Academic_FODController : Controller
    {

        private IAcademic_FODRepo _fodRepo;
        public Academic_FODController(IAcademic_FODRepo fodRepo)
        {
            _fodRepo = fodRepo;
        }
        public ActionResult LandingPage()
        {
            return PartialView(_fodRepo.GetFOD_Transaction());
        }
        public ActionResult New()
        {
            Academic_FOD model = new Academic_FOD();
            model.IsActive = true;
            return View(model);
        }
        [EncryptedActionParameter]
        public ViewResult Updation(string FodId)
        {
            Academic_FOD model = _fodRepo.GetFOD_Transaction(Convert.ToInt32(FodId));
            return View(model);
        }
        [HttpPost]
        public JsonResult Academic_FOD_CRUD(Academic_FOD model)
        {
            if (ModelState.IsValid)
            {
                Tuple<int, string> res = _fodRepo.GetFOD_CRUD(model,Session["UserId"].ToString());
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

    }
}