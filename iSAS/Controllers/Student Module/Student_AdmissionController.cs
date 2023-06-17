using ISas.Entities.RegistrationEntities;
using ISas.Repository.StudentRegistrationRepository.IRepository;
using ISas.Web.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class Student_AdmissionController : Controller
    {
        private IStudentRegistrationRepo _studentRegRepo;
        private IStudentAdmissionRepo _studentAdmissionRepo;

        public Student_AdmissionController(IStudentRegistrationRepo studentReg, IStudentAdmissionRepo admission)
        {
            this._studentRegRepo = studentReg;
            this._studentAdmissionRepo = admission;
        }
        

        [EncryptedActionParameter]
        public ActionResult NewAdmission_BasicForm(string RegID)
        {
            Student_AdmissionMaster_Model model = new Student_AdmissionMaster_Model();

            model.AddressInfo.Stud_CorrCountry = System.Configuration.ConfigurationManager.AppSettings["CountryID"].ToString();
            model.AddressInfo.Stud_CorrState = System.Configuration.ConfigurationManager.AppSettings["StateID"].ToString();
            model.AddressInfo.Stud_CorrCity = System.Configuration.ConfigurationManager.AppSettings["CityID"].ToString();
            model.Function = "SAVE";
            model.StudentInfo.AdmissionType = 'N';
            model.StudentInfo.Stud_Parents = "BO";

            if (!string.IsNullOrEmpty(RegID))
            {
                model.OfficeInfo.Stud_RegNo = RegID;
                Student_RegistrationMaster regDetails = this._studentRegRepo.GetStudentRegistrationByRegID(RegID);

                model.OfficeInfo.Session = regDetails.Sess;
                model.OfficeInfo.AdmCategoryId = regDetails.AdmCategoryId;
                model.StudentInfo.Stud_Class = regDetails.RegClassId;
                model.StudentInfo.AdmClass = regDetails.RegClassId;
                model.StudentInfo.Stud_FirstName = regDetails.Student_FirstName;
                model.StudentInfo.Stud_MiddleName = regDetails.Student_MiddleName;
                model.StudentInfo.Stud_LastName = regDetails.Student_LastName;
                model.StudentInfo.Stud_DOB = regDetails.Student_DOB;
                model.StudentInfo.Stud_Gender = regDetails.Student_Gender;
                model.FatherInfo.Father_FirstName = regDetails.Father_FirstName;
                model.FatherInfo.Father_MiddleName = regDetails.Father_MiddleName;
                model.FatherInfo.Father_LastName = regDetails.Father_LastName;
                model.FatherInfo.Father_MoblieNo = regDetails.Father_MoblieNo;

                model.MotherInfo.Mother_FirstName = regDetails.Mother_FirstName;
                model.MotherInfo.Mother_MiddleName = regDetails.Mother_MiddleName;
                model.MotherInfo.Mother_LastName = regDetails.Mother_LastName;
                model.MotherInfo.Mother_MobileNo = regDetails.Mother_MobileNo;

                model.AddressInfo.Stud_CorrAdd = regDetails.Student_CorrAdd;
                model.AddressInfo.Stud_CorrCountry = regDetails.Student_CorrCountry;
                model.AddressInfo.Stud_CorrState = regDetails.Student_CorrState;
                model.AddressInfo.Stud_CorrCity = regDetails.Student_CorrCity;
                model.AddressInfo.Stud_CorrArea = regDetails.Student_CorrArea;
                model.AddressInfo.Stud_CorrPinCode = regDetails.Student_CorrPinCode;


                model.AddressInfo.Stud_PermAdd = regDetails.Student_PermAdd;
                model.AddressInfo.Stud_PermCountry = regDetails.Student_PermCountry;
                model.AddressInfo.Stud_PermState = regDetails.Student_PermState;
                model.AddressInfo.Stud_PermCity = regDetails.Student_PermCity;
                model.AddressInfo.Stud_PermArea = regDetails.Student_PermArea;
                model.AddressInfo.Stud_PermPinCode = regDetails.Student_PermPinCode;
                
            }
            else
            {
                model.StudentInfo.Stud_Gender = 'M';
                model.OfficeInfo.Session = model.DropDownList.Session;
            }

            model.DropDownList = this._studentAdmissionRepo.GetAdmissionDropDownList(model.StudentInfo.Stud_Class,
                   model.FatherInfo.Father_Country, model.FatherInfo.Father_State,
                   model.MotherInfo.Mother_Country, model.MotherInfo.Mother_State, model.AddressInfo.Stud_CorrCountry,
                   model.AddressInfo.Stud_CorrState, model.AddressInfo.Stud_PermCountry, model.AddressInfo.Stud_PermState,
                   model.GurdianInfo.Gardian_Country, model.GurdianInfo.Gardian_State, model.AvailFacilityInfo.PRouteId, Session["SessionId"].ToString(), false);

            model.OfficeInfo.Stud_AdmNo = model.DropDownList.AdmNo;
            model.OfficeInfo.Stud_UID = model.DropDownList.ERPNo;

            return View(model);
        }

        public ActionResult Student_AdmissionLandingPage(int FilterPercent = 10)
        {
            return View(this._studentAdmissionRepo.GetAllStudent_AdmissionsList(Session["UserID"].ToString(),FilterPercent).ToList());
        }

        [EncryptedActionParameter]
        public ActionResult NewAdmission(string RegID)
        {
            Student_AdmissionMaster_Model model = new Student_AdmissionMaster_Model();

            model.AddressInfo.Stud_CorrCountry = System.Configuration.ConfigurationManager.AppSettings["CountryID"].ToString();
            model.AddressInfo.Stud_CorrState = System.Configuration.ConfigurationManager.AppSettings["StateID"].ToString();
            model.AddressInfo.Stud_CorrCity = System.Configuration.ConfigurationManager.AppSettings["CityID"].ToString();
            model.Function = "SAVE";

            model.DropDownList = this._studentAdmissionRepo.GetAdmissionDropDownList(model.StudentInfo.Stud_Class,
             model.FatherInfo.Father_Country, model.FatherInfo.Father_State,
             model.MotherInfo.Mother_Country, model.MotherInfo.Mother_State, model.AddressInfo.Stud_CorrCountry,
             model.AddressInfo.Stud_CorrState, model.AddressInfo.Stud_PermCountry, model.AddressInfo.Stud_PermState,
             model.GurdianInfo.Gardian_Country, model.GurdianInfo.Gardian_State, model.AvailFacilityInfo.PRouteId, Session["SessionId"].ToString(), false);

            model.OfficeInfo.Stud_AdmNo = model.DropDownList.AdmNo;
            model.OfficeInfo.Stud_UID = model.DropDownList.ERPNo;
            model.AvailFacilityInfo.SMS_WHOM = 'M';
            

            if (!string.IsNullOrEmpty(RegID))
            {
                model.OfficeInfo.Stud_RegNo = RegID;
                Student_RegistrationMaster regDetails = this._studentRegRepo.GetStudentRegistrationByRegID(RegID);

                model.OfficeInfo.Session = regDetails.Sess;
                model.StudentInfo.Stud_Class = regDetails.RegClassId;
                model.StudentInfo.AdmClass = regDetails.RegClassId;
                model.StudentInfo.Stud_FirstName = regDetails.Student_FirstName;
                model.StudentInfo.Stud_MiddleName = regDetails.Student_MiddleName;
                model.StudentInfo.Stud_LastName = regDetails.Student_LastName;
                model.StudentInfo.Stud_DOB = regDetails.Student_DOB;
                model.StudentInfo.Stud_Gender = regDetails.Student_Gender;
                model.FatherInfo.Father_FirstName = regDetails.Father_FirstName;
                model.FatherInfo.Father_MiddleName = regDetails.Father_MiddleName;
                model.FatherInfo.Father_LastName = regDetails.Father_LastName;
                model.FatherInfo.Father_MoblieNo = regDetails.Father_MoblieNo;

                model.MotherInfo.Mother_FirstName = regDetails.Mother_FirstName;
                model.MotherInfo.Mother_MiddleName = regDetails.Mother_MiddleName;
                model.MotherInfo.Mother_LastName = regDetails.Mother_LastName;
                model.MotherInfo.Mother_MobileNo = regDetails.Mother_MobileNo;

                model.AddressInfo.Stud_CorrAdd = regDetails.Student_CorrAdd;
                model.AddressInfo.Stud_CorrCountry = regDetails.Student_CorrCountry;
                model.AddressInfo.Stud_CorrState = regDetails.Student_CorrState;
                model.AddressInfo.Stud_CorrCity = regDetails.Student_CorrCity;
                model.AddressInfo.Stud_CorrArea = regDetails.Student_CorrArea;
                model.AddressInfo.Stud_CorrPinCode = regDetails.Student_CorrPinCode;
                

                model.AddressInfo.Stud_PermAdd = regDetails.Student_PermAdd;
                model.AddressInfo.Stud_PermCountry = regDetails.Student_PermCountry;
                model.AddressInfo.Stud_PermState = regDetails.Student_PermState;
                model.AddressInfo.Stud_PermCity = regDetails.Student_PermCity;
                model.AddressInfo.Stud_PermArea = regDetails.Student_PermArea;
                model.AddressInfo.Stud_PermPinCode = regDetails.Student_PermPinCode;
            }
            else
            {
                model.OfficeInfo.Session = model.DropDownList.Session;
            }
         

            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string ERPNo)
        {
            Student_AdmissionMaster_Model model = this._studentAdmissionRepo.GetStudent_AdmissionDetailsByERPNo(ERPNo, Session["SessionId"].ToString());

            model.Function = "UPDATE";

            model.DropDownList = this._studentAdmissionRepo.GetAdmissionDropDownList(model.StudentInfo.Stud_Class,
               model.FatherInfo.Father_Country, model.FatherInfo.Father_State,
               model.MotherInfo.Mother_Country, model.MotherInfo.Mother_State, model.AddressInfo.Stud_CorrCountry,
               model.AddressInfo.Stud_CorrState, model.AddressInfo.Stud_PermCountry, model.AddressInfo.Stud_PermState,
               model.GurdianInfo.Gardian_Country, model.GurdianInfo.Gardian_State, model.AvailFacilityInfo.PRouteId, Session["SessionId"].ToString(), model.AvailFacilityInfo.Stud_TransReq);
            
           // model.OfficeInfo.Stud_AdmNo = model.DropDownList.AdmNo;
           // model.OfficeInfo.Stud_UID = model.DropDownList.ERPNo;

            return View(model);
        }

        [HttpPost]
        public ActionResult StudentAdmission_CRUD(Student_AdmissionMaster_Model model, string SubmitType)
        {
            if (SubmitType == "Save")
            {
                if (ModelState.IsValid)
                {
                    model.UserId = Session["UserId"].ToString();
                    Tuple<int, string> res = _studentAdmissionRepo.Student_Admission_CRUD(model);
                    return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
                }

                var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
                var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
                return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
            }

            ModelState.Clear();

            model.DropDownList = this._studentAdmissionRepo.GetAdmissionDropDownList(model.StudentInfo.Stud_Class,
               model.FatherInfo.Father_Country, model.FatherInfo.Father_State,
               model.MotherInfo.Mother_Country, model.MotherInfo.Mother_State, model.AddressInfo.Stud_CorrCountry,
               model.AddressInfo.Stud_CorrState, model.AddressInfo.Stud_PermCountry, model.AddressInfo.Stud_PermState,
               model.GurdianInfo.Gardian_Country, model.GurdianInfo.Gardian_State, model.AvailFacilityInfo.PRouteId, Session["SessionId"].ToString(), model.AvailFacilityInfo.Stud_TransReq);

            if (SubmitType == "AdvanceForm")
                return View("NewAdmission", model);

            return View("NewAdmission_BasicForm", model);
        }


        public JsonResult GetModeOfTransportList(bool AvailedTransport)
        {
            return Json(_studentAdmissionRepo.GetModeOfTransportList(AvailedTransport), JsonRequestBehavior.AllowGet);
        }
    }
}
