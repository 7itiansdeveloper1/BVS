using ISas.Entities.StaffEntities;
using ISas.Repository.Academic.IRepository;
using ISas.Repository.StaffRepository.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Staff
{
    [Authorize]
    [ExceptionHandler]
    public class Staff_AttendanceRegisterController : Controller
    {
        private IStaff_AttendanceRegisterRepo _attenRegRepo;
        private IAcademic_DepartmentMasterRepo _deptMstList;
        public Staff_AttendanceRegisterController(IStaff_AttendanceRegisterRepo attenRegRepo, IAcademic_DepartmentMasterRepo deptMstRepo)
        {
            _attenRegRepo = attenRegRepo;
            _deptMstList = deptMstRepo;
        }


        // GET: Staff_AttendanceRegister
        public ViewResult Staff_AttendanceRegister_Filter()
        {
            Staff_AttendanceRegisterModels model = new Staff_AttendanceRegisterModels();
            model.DeparmentList = _deptMstList.GetDepartmentList().Select(r => new SelectListItem
            {
                Text = r.DeptName,
                Value = r.DeptID,
            }).ToList();
            model.AttenDate = DateTime.Now.Date.ToShortDateString().Replace("-", "/");
            return View(model);
        }

        public PartialViewResult _Staff_AttendanceRegister_Details(string AttenDate, string DeptIds)
        {
            Staff_AttendanceRegisterModels model = new Staff_AttendanceRegisterModels();
            model.StaffAttendanceDetails = _attenRegRepo.GetStaffAttenDetails(AttenDate, DeptIds);
            return PartialView(model);
        }

        [HttpPost]
        public JsonResult Staff_AttendanceRegister_CRUD(Staff_AttendanceRegisterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.SessionId = Session["SessionId"].ToString();
                Tuple<int, string> res = _attenRegRepo.Staff_AttendanceRegister_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}