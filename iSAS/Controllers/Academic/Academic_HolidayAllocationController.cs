using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_HolidayAllocationController : Controller
    {
        private IAcademic_HolidayAllocationRepo _allocationRepo;
        private IStudentClass _studentClassRepos;
        public Academic_HolidayAllocationController(IAcademic_HolidayAllocationRepo allopcationRepo, IStudentClass ClassRepo)
        {
            _allocationRepo = allopcationRepo;
            _studentClassRepos = ClassRepo;
        }

        // GET: Academic_HolidayAllocation
        public ActionResult LandingPage()
        {
            ViewBag.sessionName = Session["SessionName"];
            return View(_allocationRepo.GetHolidayAllocationList());
        }

        [EncryptedActionParameter]
        public ActionResult HolidaySetUp(string FDate, string TDate, string HolidayName, string HolidayId, string NoofDays)
        {
            Academic_HolidayAllocationModel model = new Academic_HolidayAllocationModel();
            model.FDate = FDate;model.TDate = TDate;model.HolidayName = HolidayName;
            model.HolidayId = HolidayId; model.NoofDays = Convert.ToInt32(NoofDays);
            model = _allocationRepo.GetStaffList(model);
           // model.StaffList = _allocationRepo.GetStaffList();
           //model.DepartmentList = _allocationRepo.GetDepartmentList();
           //model.ClassList = _studentClassRepos.GetAllClasses(Session["UserID"].ToString()).OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
           //{
           //    Text = r.ClassName,
           //    Value = r.ClassId,
           //}).ToList(); 
            return View(model);
        }

        [HttpPost]
        public JsonResult Academic_HolidayAllocation_CRUD(Academic_HolidayAllocationModel model)
        {
            if (ModelState.IsValid)
            {
                string refIds = "";

                if (model.CRUDFor == "STAFF")
                    refIds = string.Join(",", model.StaffList.Where(r => r.Selected).Select(r => r.StaffID));

                else if (model.CRUDFor == "STUD")
                    refIds = string.Join(",", model.ClassList.Where(r => r.Selected).Select(r => r.Value));


                model.CRUDMode = "SAVE";
                string messege = "";
                messege = _allocationRepo.Academic_HolidayAllocation_CRUD(model.HolidayId, refIds, model.CRUDFor);

                return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
    }
}