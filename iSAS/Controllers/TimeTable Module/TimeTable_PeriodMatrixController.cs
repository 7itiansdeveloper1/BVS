using ISas.Entities.TimeTable_Entities;
using ISas.Repository.TimeTable_Repo.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.TimeTable_Module
{
    [Authorize]
    [ExceptionHandler]
    public class TimeTable_PeriodMatrixController : Controller
    {
        private ITimeTable_PeriodMatrixRepo _matrixRepo;
        public TimeTable_PeriodMatrixController(ITimeTable_PeriodMatrixRepo matrixRepo)
        {
            _matrixRepo = matrixRepo;
        }

        // GET: TimeTable_PeriodMatrix
        public PartialViewResult LandingPage()
        {
            return PartialView(_matrixRepo.GetMatrixList(""));
        }

        public ActionResult New()
        {
            TimeTable_PeriodMatrixModels model = new TimeTable_PeriodMatrixModels();
            model.IsActive = true;
            model.PrintOrder = 1;
            return View(model);
        }

        public ActionResult Updation(string MatrixId)
        {
            return View(_matrixRepo.GetMatrixById(MatrixId));
        }

        [HttpPost]
        public JsonResult TimeTable_PeriodMatrix_CRUD(TimeTable_PeriodMatrixModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _matrixRepo.TimeTable_PeriodMatrix_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TimeTable_PeriodMatrix_DELETE(string MatrixId)
        {
            Tuple<int, string> res = _matrixRepo.TimeTable_PeriodMatrix_CRUD(MatrixId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }


        #region Period Matrix Class Setup
        public PartialViewResult _PeriodMatrixClassSetup(string MatrixId)
        {
            return PartialView(_matrixRepo.PeriodMatrixClassSetup_FormLoad(MatrixId));
        }

        public JsonResult PeriodMatrixClassSetup_CRUD(PeriodMatrixClassSetupModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _matrixRepo.PeriodMatrixClassSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}