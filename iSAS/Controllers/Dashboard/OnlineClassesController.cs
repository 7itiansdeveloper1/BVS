using ISas.Entities.DashboardEntities;
using ISas.Repository.DashboardRepository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace ISas.Web.Controllers.Dashboard
{
    public class OnlineClassesController : Controller
    {
        // GET: OnlineClasses
        private IOnlineClass_StudentRepo _onlineClass_StudentRepo;
        public OnlineClassesController(IOnlineClass_StudentRepo onlineClass_StudentRepo)
        {
            _onlineClass_StudentRepo = onlineClass_StudentRepo;
        }
        public ActionResult Class1()
        {
            return View(_onlineClass_StudentRepo.GetStudentOnlineClassList(Session["UserId"].ToString()));
        }


        public JsonResult authenticateuser(string username,string password )
        {
            bool success = WebSecurity.Login(username, password, false);
            return new JsonResult { Data = success, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        public JsonResult punchTime(string classid)
        {
            Tuple<bool, int, string> response = _onlineClass_StudentRepo.onlineLogPunch_CRUD(Session["UserId"].ToString(), classid);
            return new JsonResult { Data = response.Item1, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}