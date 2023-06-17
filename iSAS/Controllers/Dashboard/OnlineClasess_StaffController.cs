using ISas.Entities.DashboardEntities;
using ISas.Repository.DashboardRepository.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Dashboard
{
    [Authorize]
    [ExceptionHandler]
    public class OnlineClasess_StaffController : Controller
    {
        private IOnlineClass_StaffRepo _onlineClass_StaffRepo;

        public OnlineClasess_StaffController(IOnlineClass_StaffRepo onlineClass_StaffRepo)
        {
            _onlineClass_StaffRepo = onlineClass_StaffRepo;
        }
        public ActionResult Index()
        {
            return View(_onlineClass_StaffRepo.OnlineClasses_Transaction_FormLoad(Session["UserId"].ToString()));
        }
        public ActionResult _dayOnlineClass(string onlineclassdate )
        {
            return PartialView(_onlineClass_StaffRepo.OnlineClasses_Transaction_ClassList(Session["UserId"].ToString(),onlineclassdate));
        }
        [HttpPost]
        public JsonResult OnlineClasess_Staff_CRUD(OnlineClass_Staff model)
        {
            if (ModelState.IsValid)
            {
                model.object_onlineclass.teacherId= Session["UserID"].ToString();
                Tuple<int, string> res = this._onlineClass_StaffRepo.OnlineClasses_CRUD(model.object_onlineclass);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult ZoomURL_CRUD(string zoomurl)
        {
            if (ModelState.IsValid)
            {
                Tuple<int, string> res = this._onlineClass_StaffRepo.ZoomURL_CRUD(Session["UserID"].ToString(),zoomurl);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult OnlineClass_DELETE(string classId)
        {
            
            onlineclass model = new onlineclass();
            model.id = new Guid(classId)  ;
            model.classId = "CLS0000000SEC00";
            model.onlineClassDate = DateTime.Today.Date.ToShortDateString();
            model.onlineClassStartTime = "12:00PM";
            Tuple<int, string> res = this._onlineClass_StaffRepo.OnlineClasses_CRUD(model);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }

    }
}