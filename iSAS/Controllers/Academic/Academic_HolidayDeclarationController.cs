using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_HolidayDeclarationController : Controller
    {
        private IAcademic_HolidayDeclarationRepo _holidayDecRepo;
        public Academic_HolidayDeclarationController(IAcademic_HolidayDeclarationRepo holidayDecRepo)
        {
            _holidayDecRepo = holidayDecRepo;
        }

        public ActionResult LandingPage()
        {
            return View(_holidayDecRepo.GetHolidayDeclarationList(null));
        }

        [EncryptedActionParameter]
        public ActionResult New(string HolidayId)
        {
            return View(_holidayDecRepo.GetHolidayDeclarationById(HolidayId));
        }

        //[EncryptedActionParameter]
        //public ActionResult Updation(string FDate, string TDate, string HolidayName, string HolidayId, string NoofDays)
        //{
        //    Academic_HolidayDeclarationModel model = new Academic_HolidayDeclarationModel();
        //    model.Fdate = FDate;model.TDate = TDate;model.HolidayId = HolidayId;
        //    model.HolidayName = HolidayName;model.NoofDays = Convert.ToInt32(NoofDays);
        //    return View(model);
        //}

        [HttpPost]
        public JsonResult Academic_HolidayDeclaration_CRUD(Academic_HolidayDeclarationModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.CRUDMode = "SAVE";
                string messege = "";
                messege = _holidayDecRepo.Academic_HolidayDeclaration_CRUD(model);

                return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_HolidayDeclaration_Delete(string HolidayId)
        {
            string messege = _holidayDecRepo.Academic_HolidayDeclaration_CRUD(HolidayId);
            return Json(new { status = "success", Msg = messege }, JsonRequestBehavior.AllowGet);
        }
    }
}