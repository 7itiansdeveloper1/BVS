using ISas.Entities.UserEntities;
using ISas.Repository.UserRepo.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.UserModule
{
    [Authorize]
    [ExceptionHandler]
    public class UserCreationController : Controller
    {
        private IUserCreationRepo _userCreationRepo;
        public UserCreationController(IUserCreationRepo userCreationRepo)
        {
            _userCreationRepo = userCreationRepo;
        }

        // GET: UserCreation
        public PartialViewResult LandingPage(string UserType)
        {
            return PartialView(_userCreationRepo.UserCreation_FormLoad(UserType));
        }

        // GET: UserCreation
        public ActionResult New()
        {
            return View(_userCreationRepo.UserCreation_FormLoad("Staff"));
        }

        public JsonResult GetUserRoleList(string UserType)
        {
            return Json(_userCreationRepo.UserCreation_FormLoad(UserType).UserRoleList, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _changeRole(string userrefrenceNo)
        {
            UserCreationModels model = _userCreationRepo.UserCreation_FormLoad("UserRole");
            model.SelectedUserOrStaffID = userrefrenceNo;
            return PartialView(model);
        }

        //public JsonResult UserCreation_CRUD(UserCreationModels model)
        [HttpPost]
        public JsonResult UserCreation_CRUD(string userReferenceNo, string userType, string roleId,string mode)
        {
            if (ModelState.IsValid)
            {
                //model.UserId = Session["UserId"].ToString();
                //Tuple<int, string> res = _userCreationRepo.UserCreation_CRUD(model);
                Tuple<int, string> res = _userCreationRepo.UserCreation_CRUD( userReferenceNo, userType, roleId, Session["UserId"].ToString(), mode);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}