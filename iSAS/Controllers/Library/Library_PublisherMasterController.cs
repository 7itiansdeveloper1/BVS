using ISas.Entities.LibraryEntities;
using ISas.Repository.Interface;
using ISas.Repository.Library.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Library
{
    [Authorize]
    [ExceptionHandler]
    public class Library_PublisherMasterController : Controller
    {
        private ILibrary_PublisherMasterRepo _publisherRepo;
        private ICountryRepository _countryRepo;
        private IStateRepository _stateRepo;
        private ICityRepository _cityRepo;
        public Library_PublisherMasterController(ILibrary_PublisherMasterRepo publisherRepo, ICountryRepository
            countryRepo, IStateRepository stateRepo, ICityRepository cityRepo)
        {
            _publisherRepo = publisherRepo;
            _countryRepo = countryRepo;
            _stateRepo = stateRepo;
            _cityRepo = cityRepo;
        }

        public PartialViewResult LandingPage()
        {
            return PartialView(_publisherRepo.GetPublisherList(""));
        }

        public ActionResult New()
        {
            Library_PublisherMasterModels model = new Library_PublisherMasterModels();
            model.IsActive = true;

            model.CountryList = _countryRepo.GetAllCountryList().OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
            {
                Text = r.CountryName,
                Value = r.CountryID
            }).ToList();

            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string PublisherId)
        {
            Library_PublisherMasterModels model = _publisherRepo.GetPublisherById(PublisherId);
            model.CountryList = _countryRepo.GetAllCountryList().OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
            {
                Text = r.CountryName,
                Value = r.CountryID
            }).ToList();

            model.StateList = _stateRepo.GetStateListByCountryID(model.PublisherCountry).OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
            {
                Text = r.StateName,
                Value = r.StateID
            }).ToList();

            model.CityList = _cityRepo.GetCityListByStateID(model.PublisherState).OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
            {
                Text = r.CityName,
                Value = r.CityID
            }).ToList();

            return View(model);
        }

        public PartialViewResult _BookTitleWiseReport(string PublisherId)
        {
            Tuple<List<Library_Author_BookTitleWiseReportModel>, string> result = _publisherRepo.Get_BookTitleWiseReport(PublisherId);
            ViewBag.reportName = result.Item2;
            return PartialView("~/Views/Library_AuthorMaster/_BookTitleWiseReport.cshtml", result.Item1);
        }

        [HttpPost]
        public JsonResult Library_PublisherMaster_CRUD(Library_PublisherMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserID"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _publisherRepo.Library_PublisherMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Library_PublisherMaster_DELETE(string PublisherId)
        {
            Tuple<int, string> res = _publisherRepo.Library_PublisherMaster_CRUD(PublisherId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}