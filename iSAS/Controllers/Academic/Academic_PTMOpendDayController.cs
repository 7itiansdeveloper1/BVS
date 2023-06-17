using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_PTMOpendDayController : Controller
    {
        private IAcademic_PTMOpendDayRepo _openDayRepo;
        private IStudentClass _studentClassRepos;
        public Academic_PTMOpendDayController(IAcademic_PTMOpendDayRepo openDayRepo, IStudentClass classRepo)
        {
            _openDayRepo = openDayRepo;
            _studentClassRepos = classRepo;
        }
        // GET: Academic_PTMOpendDay
        public PartialViewResult _LandingPage(string ClassId, string Category)
        {
            return PartialView(_openDayRepo.GetOpenDayList(Session["SessionID"].ToString(), ClassId, Session["UserID"].ToString(), Category));
        }

        public ViewResult New()
        {
            Academic_PTMOpendDayEntities model = new Academic_PTMOpendDayEntities();
            model.AttenCategoryList.Add(new SelectListItem {Text = "PTM", Value  = "PTM", Selected = true });
            model.AttenCategoryList.Add(new SelectListItem { Text = "ORIENTATION", Value = "ORIENTATION", Selected = false });

            var classes = _studentClassRepos.GetAllClasses(); //Session["UserID"].ToString()
            if (classes != null && classes.Count() > 0)
                model.ClassList = classes.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.ClassName,
                    Value = r.ClassId,
                }).ToList();

            model.CopyToClassList = model.ClassList;
            return View(model);
        }

        [HttpPost]
        public JsonResult Academic_PTMOpendDay_CRUD(Academic_PTMOpendDayEntities model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                model.SessionId = Session["SessionID"].ToString();

                Tuple<int, string> res = _openDayRepo.Academic_PTMOpendDay_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_PTMOpendDay_Delete(Academic_PTMOpendDayEntities model)
        {
            model.CRUDMode = "DELETE";
            model.UserId = Session["UserId"].ToString();
            model.SessionId = Session["SessionID"].ToString();

            Tuple<int, string> res = _openDayRepo.Academic_PTMOpendDay_CRUD(model);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_PTMOpendDay_CopyToClass(string Category, string FromClassId, string ToClass)
        {
            Tuple<int, string> res = _openDayRepo.Academic_PTMOpendDay_CopyToClass(Session["SessionId"].ToString(), Session["UserId"].ToString(), Category, FromClassId, ToClass);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}