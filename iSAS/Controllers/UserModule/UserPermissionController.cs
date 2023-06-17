using ISas.Entities.UserEntities;
using ISas.Repository.UserRepo.IRepository;
using ISas.Web.Models;
using System.Linq;
using System.Web.Mvc;
using System;


namespace ISas.Web.Controllers.UserModule
{
    [Authorize]
    [ExceptionHandler]
    public class UserPermissionController : Controller
    {
        private IUserPermissionRepo _userPermission;
        public UserPermissionController(IUserPermissionRepo userPermission)
        {
            _userPermission = userPermission;
        }

        // GET: UserPermission
        [EncryptedActionParameter]
        public ActionResult New(string Name, string RefId, string UserType)
        {
            UserPermissionModels model = new UserPermissionModels();
            model.ModuleList = _userPermission.ModuleList();
            model.SelectedARefName = Name;
            model.UserReferenceNo = RefId;
            model.UserType = UserType;
            return View(model);
        }
        public PartialViewResult _ModuleRoleDetail(string ModuleId, string UserRefId)
        {
            return PartialView(_userPermission.ModuleRoleList(ModuleId, UserRefId));
        }

        [HttpPost]
        public JsonResult UserPermission_CRUD(UserPermissionModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.Mode = "SAVE";
                string messege = "";
                messege = _userPermission.UserPermission_CRUD(model);

                return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UserRoleMaster_CRUD(string roleId,string isActive)
        {

                Tuple<int, string> res = _userPermission.UserRoleMaster_CRUD(roleId, Convert.ToBoolean(isActive),Session["UserId"].ToString() );
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}