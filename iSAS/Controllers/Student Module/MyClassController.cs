using ISas.Entities.Student_Entities;
using ISas.Repository.StudentRepository.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Student_Module
{
    [Authorize]
    [ExceptionHandler]
    public class MyClassController : Controller
    {
        private  IMyClassRepo _myClassRepo;

        public MyClassController(IMyClassRepo myClassRepo)
        {
            _myClassRepo = myClassRepo;
        }
        public ActionResult New(string status = "ACTIVE")
        {
            MyClassModels model = _myClassRepo.MyClass_Transaction(Session["UserId"].ToString(), Session["SessionId"].ToString(), status);
            model.status = "ACTIVE";
            return View(model);
        }

        [HttpPost]
        public JsonResult UserCreation_CRUD(string erpNo)
        {
            Tuple<int, string> res = _myClassRepo.UserCreation_CRUD(erpNo, "RESETPWD");
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}