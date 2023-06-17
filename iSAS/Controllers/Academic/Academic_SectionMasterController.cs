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
    public class Academic_SectionMasterController : Controller
    {
        private IAcademic_SectionMasterRepo _sectionMstRepo;
        public Academic_SectionMasterController(IAcademic_SectionMasterRepo sectionMst)
        {
            _sectionMstRepo = sectionMst;
        }

        // GET: Fee_FeeHeadMaster
        public ActionResult LandingPage()
        {
            return PartialView(_sectionMstRepo.GetAcademic_SectionMasterList(""));
        }

        public ActionResult New()
        {
            Academic_SectionMasterModels model = new Academic_SectionMasterModels();
            model.Active = true;
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string SecId)
        {
            return View(_sectionMstRepo.GetAcademic_SectionMasterById(SecId));
        }

        [HttpPost]
        public JsonResult Academic_SectionMaster_CRUD(Academic_SectionMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _sectionMstRepo.Academic_SectionMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_SectionMaster_Delete(string SecId)
        {
            Tuple<int, string> res = _sectionMstRepo.Academic_SectionMaster_CRUD(SecId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}