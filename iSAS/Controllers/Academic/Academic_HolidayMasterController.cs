using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using ISas.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_HolidayMasterController : Controller
    {
        private IAcademic_HolidayMasterRepo _holidayMasterRepo;
        public Academic_HolidayMasterController(IAcademic_HolidayMasterRepo HolidayMasterRepo)
        {
            _holidayMasterRepo = HolidayMasterRepo;
        }

        // GET: Fee_FeeHeadMaster
        public ActionResult LandingPage()
        {
            return PartialView(_holidayMasterRepo.GetHolidayMasterList());
        }

        public ActionResult New()
        {
            Academic_HolidayMasterModel model = new Academic_HolidayMasterModel();
            model.IsActive = true;

            return View(model);
        }

        [HttpPost]
        public JsonResult Academic_HolidayMaster_CRUD(Academic_HolidayMasterModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                string messege = "";
                messege = _holidayMasterRepo.Academic_HolidayMaster_CRUD(model);

                return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_HolidayMaster_Delete(string HolidayId)
        {
            string messege = _holidayMasterRepo.Academic_HolidayMaster_CRUD(HolidayId);
            return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
        }
    }
}