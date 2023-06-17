using ISas.Entities.RegistrationEntities;
using ISas.Repository.Interface;
using ISas.Repository.StudentRegistrationRepository.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Data;

namespace ISas.Web.Controllers.Student_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Student_Registration1Controller : Controller
    {
        private IStudentClass _studentClassRepos;
        private IStudentSession _studentSessionRepos;
        private ICountryRepository _countryRepo;
        private IStateRepository _stateRepo;
        private ICityRepository _cityRepo;

        private IAreaRepository _areaRepo;
        private ISchool_DocumentMasterRepo _schoolDocRepo;
        private IStudentRegistrationRepo _studentRegRepo;

        private ISchoolAdmissionCategoryRepo _studentAdmCategoryRepo;


        public Student_Registration1Controller(IStudentClass studentClass, IStudentSession studentSession, ICountryRepository country, IAreaRepository area, ISchool_DocumentMasterRepo schoolDoc, IStudentRegistrationRepo studentReg, ISchoolAdmissionCategoryRepo admCategory, IStateRepository states, ICityRepository cities)
        {
            this._studentClassRepos = studentClass;
            this._studentSessionRepos = studentSession;
            this._countryRepo = country;
            this._areaRepo = area;
            this._schoolDocRepo = schoolDoc;
            this._studentRegRepo = studentReg;
            this._studentAdmCategoryRepo = admCategory;
            this._stateRepo = states;
            this._cityRepo = cities;
        }


        public ActionResult Student_RegistrationLandingPage()
        {
            return View(this._studentRegRepo.Student_Registration_MasterDetails("").ToList());
        }

        
        public ActionResult NewRegistration()
        {
            Student_RegistrationMaster model = new Student_RegistrationMaster();


            model.Student_CorrCountry = System.Configuration.ConfigurationManager.AppSettings["CountryID"].ToString();
            model.Student_CorrState = System.Configuration.ConfigurationManager.AppSettings["StateID"].ToString();
            model.Student_CorrCity = System.Configuration.ConfigurationManager.AppSettings["CityID"].ToString();
            Tuple<string, string> autNos = this._studentRegRepo.GetReleatedAutoNos(Session["SessionId"].ToString());
            model.RegID = autNos.Item1;
            model.FormID = autNos.Item2;

            model.Amt = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["RegAmount"]);
            model.IntrvwDate = System.Configuration.ConfigurationManager.AppSettings["InterViewDate"].ToString();
            model.RegDate = DateTime.Now.ToShortDateString().Replace("-", "/");

            var admCategoryies = this._studentAdmCategoryRepo.GetAllAdmissionCategoryList();
            if (admCategoryies != null && admCategoryies.Count() > 0)
                model.SchoolAdimCategories = admCategoryies.OrderBy(p => p.PrintOrder).Select(p => new SelectListItem
                {
                    Text = p.AdmCategoryName,
                    Value = p.AdmCategoryId,
                    Selected = p.IsDefault
                }).ToList();


            var sessions = this._studentSessionRepos.GetAllSessions();
            if (sessions != null && sessions.Count() > 0)
                model.SessionList = sessions.OrderByDescending(p => p.PrintOrder).Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId,
                }).ToList();

            if (sessions.Count() > 0)
            {
                model.Sess = sessions.OrderByDescending(p => p.PrintOrder).FirstOrDefault().SessId;
            }
            var classes = this._studentClassRepos.GetAllClasses(Session["UserID"].ToString());
            if (classes != null && classes.Count() > 0)
                model.ClassList = classes.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.ClassName,
                    Value = r.ClassId,
                    Selected = r.ClassName.ToUpper().Replace(" ", "").Contains("PRE-NUR")
                }).ToList();

            var countries = this._countryRepo.GetAllCountryList();
            if (countries != null && countries.Count() > 0)
                model.CountryList = countries.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.CountryName,
                    Value = r.CountryID
                }).ToList();

            var areaList = this._areaRepo.GetAllAreaList();
            if (areaList != null && areaList.Count() > 0)
                model.AreaList = areaList.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.AreaName,
                    Value = r.AreaID
                }).ToList();

            var corres_states = this._stateRepo.GetStateListByCountryID(model.Student_CorrCountry);
            if (corres_states != null && corres_states.Count() > 0)
                model.Corres_StateList = corres_states.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.StateName,
                    Value = r.StateID
                }).ToList();

            var corres_cities = this._cityRepo.GetCityListByStateID(model.Student_CorrState);
            if (corres_cities != null && corres_cities.Count() > 0)
                model.Corres_CityList = corres_cities.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.CityName,
                    Value = r.CityID
                }).ToList();


            model.DocList = this._schoolDocRepo.GetAllStudentDocumentList().ToList();
            return View(model);
        }


        [EncryptedActionParameter]
        public ActionResult Updation(string RegID)
        {
            Student_RegistrationMaster model = this._studentRegRepo.GetStudentRegistrationByRegID(RegID);
            var sessions = this._studentSessionRepos.GetAllSessions();


            var admCategoryies = this._studentAdmCategoryRepo.GetAllAdmissionCategoryList();
            if (admCategoryies != null && admCategoryies.Count() > 0)
                model.SchoolAdimCategories = admCategoryies.OrderByDescending(p => p.PrintOrder).Select(p => new SelectListItem
                {
                    Text = p.AdmCategoryName,
                    Value = p.AdmCategoryId,
                    Selected = p.IsDefault
                }).ToList();


            if (sessions != null && sessions.Count() > 0)
                model.SessionList = sessions.OrderByDescending(p => p.PrintOrder).Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId
                }).ToList();


            var classes = this._studentClassRepos.GetAllClasses(Session["UserID"].ToString());
            if (classes != null && classes.Count() > 0)
                model.ClassList = classes.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.ClassName,
                    Value = r.ClassId
                }).ToList();

            var countries = this._countryRepo.GetAllCountryList();
            if (countries != null && countries.Count() > 0)
                model.CountryList = countries.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.CountryName,
                    Value = r.CountryID
                }).ToList();


            var corres_states = this._stateRepo.GetStateListByCountryID(model.Student_CorrCountry);
            if (corres_states != null && corres_states.Count() > 0)
                model.Corres_StateList = corres_states.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.StateName,
                    Value = r.StateID
                }).ToList();

            var corres_cities = this._cityRepo.GetCityListByStateID(model.Student_CorrState);
            if (corres_cities != null && corres_cities.Count() > 0)
                model.Corres_CityList = corres_cities.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.CityName,
                    Value = r.CityID
                }).ToList();


            if (!string.IsNullOrEmpty(model.Student_PermState))
            {
                var perm_states = this._stateRepo.GetStateListByCountryID(model.Student_PermCountry);
                if (perm_states != null && perm_states.Count() > 0)
                    model.Perm_StateList = perm_states.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                    {
                        Text = r.StateName,
                        Value = r.StateID
                    }).ToList();
            }

            if (!string.IsNullOrEmpty(model.Student_PermCity))
            {
                var perm_cities = this._cityRepo.GetCityListByStateID(model.Student_PermState);
                if (perm_cities != null && perm_cities.Count() > 0)
                    model.Perm_CityList = perm_cities.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                    {
                        Text = r.CityName,
                        Value = r.CityID
                    }).ToList();

            }


            var areaList = this._areaRepo.GetAllAreaList();
            if (areaList != null && areaList.Count() > 0)
                model.AreaList = areaList.OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
                {
                    Text = r.AreaName,
                    Value = r.AreaID
                }).ToList();

            model.DocList = this._schoolDocRepo.GetAllStudentDocumentList().ToList();

            return View(model);
        }

        [HttpPost]
        public JsonResult StudentRegistration_CRUD(Student_RegistrationMaster model)
        {
            Tuple<bool, string> dobvalidationResult = ValidateStudentDOB(model.Student_DOB, model.RegClassId);

            if (!dobvalidationResult.Item1)
                ModelState.AddModelError("Student_DOB", dobvalidationResult.Item2);

            if (ModelState.IsValid)
            {
                Tuple<int, string> res = this._studentRegRepo.Student_Registration_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning", RegId = model.RegID }, JsonRequestBehavior.AllowGet);
            }

            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        private Tuple<bool, string> ValidateStudentDOB(string DOB, string classId)
        {
            DateTime dateAsOn = Convert.ToDateTime(System.Configuration.ConfigurationManager.AppSettings["DateAsOn"]);
            string dateAsOnDisplay = System.Configuration.ConfigurationManager.AppSettings["DateAsOnDisplay"];
            DataTable dt = _studentRegRepo.ValidateDOB(classId, DOB, dateAsOn);
            string Y, M, D;
            bool isdobvalid;
            Y = dt.Rows[0][0].ToString();
            M = dt.Rows[0][1].ToString();
            D = dt.Rows[0][2].ToString();
            isdobvalid = Convert.ToBoolean(dt.Rows[0][3]);
            string lowerAge = dt.Rows[0][4].ToString();
            string upperAge = dt.Rows[0][5].ToString();
            try
            {
                if (isdobvalid)
                    return new Tuple<bool, string>(true, "success");
                return new Tuple<bool, string>(false, string.Format("Age Is {0} Y , {1} M , {2} D. And it should between {3} Y to {4} Y as on {5}", Y, M, D, lowerAge, upperAge, dateAsOnDisplay));
            }
            catch
            {
                return new Tuple<bool, string>(false, string.Format("DOB Invalid, it should between {0} Y to {1} Y as on {2}", lowerAge, upperAge, dateAsOnDisplay));
            }
            //return new Tuple<bool, string>(false, string.Format("DOB Invalid, it should between {0} Y to {1} Y as on {2}", lowerAge, upperAge, dateAsOn));
        }


        [HttpGet]
        public ViewResult RegistrationSlip_Print(string RegId)
        {
            return View(_studentRegRepo.RegistrationSlipDetails(RegId));
        }
    }
}