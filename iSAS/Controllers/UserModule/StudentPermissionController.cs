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
    public class StudentPermissionController : Controller
    {

        private IStudentPermissionRepo _studentPermissionRepo;

        public StudentPermissionController(IStudentPermissionRepo studentPermissionRepo)
        {
            _studentPermissionRepo = studentPermissionRepo;
        }
        public ActionResult StudentRoleAssignment()
        {
            StudentRoleModels studentRoleModels = new StudentRoleModels();
            studentRoleModels = _studentPermissionRepo.StudentRoleAssign_Transaction_FormLoad();
            return View(studentRoleModels);
        }
        public PartialViewResult _StudentRoleAssignmentList(string classId)
        {
            return PartialView(_studentPermissionRepo.StudentRoleAssign_Transaction_GetRoleList(classId));
        }
        [HttpPost]
        public JsonResult StudentRoleAssignment_CRUD(StudentRoleModels model)
        {
            if (ModelState.IsValid)
            {
                model.userId = Session["UserId"].ToString();
                string messege = "";
                messege = _studentPermissionRepo.StudentRoleAssign_CRUD(model);
                return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}