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
    public class Library_SupplierMasterController : Controller
    {
        private ILibrary_SupplierMasterRepo _supplierMstRepo;
        private ICountryRepository _countryRepo;
        private IStateRepository _stateRepo;
        private ICityRepository _cityRepo;
        public Library_SupplierMasterController(ILibrary_SupplierMasterRepo supplierRepo, ICountryRepository
            countryRepo, IStateRepository stateRepo, ICityRepository cityRepo)
        {
            _supplierMstRepo = supplierRepo;
            _countryRepo = countryRepo;
            _stateRepo = stateRepo;
            _cityRepo = cityRepo;
        }

        public PartialViewResult LandingPage()
        {
            return PartialView(_supplierMstRepo.GetSupplierList(""));
        }

        public ActionResult New()
        {
            Library_SupplierMasterModels model = new Library_SupplierMasterModels();
            model.IsActive = true;

            model.CountryList = _countryRepo.GetAllCountryList().OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
            {
                Text = r.CountryName,
                Value = r.CountryID
            }).ToList();

            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string SupplierId)
        {
            Library_SupplierMasterModels model = _supplierMstRepo.GetSupplierById(SupplierId);
            model.CountryList = _countryRepo.GetAllCountryList().OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
            {
                Text = r.CountryName,
                Value = r.CountryID
            }).ToList();

            model.StateList = _stateRepo.GetStateListByCountryID(model.SupplierCountry).OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
            {
                Text = r.StateName,
                Value = r.StateID
            }).ToList();

            model.CityList = _cityRepo.GetCityListByStateID(model.SupplierState).OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
            {
                Text = r.CityName,
                Value = r.CityID
            }).ToList();

            return View(model);
        }

        public PartialViewResult _BookTitleWiseReport(string SupplierId)
        {
            Tuple<List<Library_Author_BookTitleWiseReportModel>, string> result = _supplierMstRepo.Get_BookTitleWiseReport(SupplierId);
            ViewBag.reportName = result.Item2;
            return PartialView("~/Views/Library_AuthorMaster/_BookTitleWiseReport.cshtml", result.Item1);
        }


        [HttpPost]
        public JsonResult Library_SupplierMaster_CRUD(Library_SupplierMasterModels model)
        {
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserID"].ToString();
                model.CRUDMode = "SAVE";
                
                Tuple<int, string> res = _supplierMstRepo.Library_SupplierMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Library_SupplierMaster_DELETE(string SupplierId)
        {
            Tuple<int, string> res = _supplierMstRepo.Library_SupplierMaster_CRUD(SupplierId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}