using ISas.Entities.StaffEntities;
using ISas.Entities.TimeTable_Entities;
using ISas.Repository.Interface;
using ISas.Repository.StaffRepository.IRepository;
using ISas.Web.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Staff
{
    [Authorize]
    [ExceptionHandler]
    public class Staff_StaffDetailMasterController : Controller
    {
        private IStaff_StaffDetailMasterRepo _staffRepo;
        private IStaff_DocumentUploadRepo _staffDocRepo;
        private IStudentAttendance _studentAttendanceRepos;

        //private CommonController commonContr;

        public Staff_StaffDetailMasterController(IStaff_StaffDetailMasterRepo staffRepo, IStudentAttendance studAttn,IStaff_DocumentUploadRepo staffDocRepo)
        {
            this._staffRepo = staffRepo;
            //this.commonContr = cntrl;
            _studentAttendanceRepos = studAttn;
            _staffDocRepo = staffDocRepo;
        }

        [EncryptedActionParameter]
        public ActionResult Staff_StaffDetailMasterLandingPage(string IncludeInactive = "0")
        {
            ViewBag.incInactive = IncludeInactive;
            return View("Staff_StaffDetailMasterLandingPage", _staffRepo.GetAllStaff_StaffDetailMasterList(null, IncludeInactive));
        }

        public ActionResult NewStaff_StaffDetailMaster()
        {
            Staff_StaffDetailMasterModel model = new Staff_StaffDetailMasterModel();
            model.CorrCountry = System.Configuration.ConfigurationManager.AppSettings["CountryID"].ToString();
            model.CorrState = System.Configuration.ConfigurationManager.AppSettings["StateID"].ToString();
            model.CorrCity = System.Configuration.ConfigurationManager.AppSettings["CityID"].ToString();
            model.Function = "SAVE";
            model.DropDownList = this._staffRepo.GetStaffDetailMasterDropDownList(model.CorrCountry, model.CorrState,
                model.PermCountry, model.PermState);
            model.StaffCode = model.DropDownList.NewEmpcode;
            model.StaffID = model.DropDownList.NewStaffId;
            model.StaffImg = "Images/System/defaultUserImg.png";
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string StaffID)
        {
            Staff_StaffDetailMasterModel model = this._staffRepo.GetStaff_StaffDetailMasterByStaffID(StaffID);
            model.Function = "UPDATE";
            model.DropDownList = this._staffRepo.GetStaffDetailMasterDropDownList(model.CorrCountry, model.CorrState,
                model.PermCountry, model.PermState);
            return View(model);
        }

        [HttpPost]
        public JsonResult Staff_StaffDetailMaster_CRUD(Staff_StaffDetailMasterModel model, HttpPostedFileBase file)
        {
            
            string viewName = "SelfUpdation";
            if (Session["UserId"].ToString()=="1")
            {
                viewName=model.Function == "SAVE" ? "NewStaff_StaffDetailMaster" : "Staff_StaffDetailMasterLandingPage";
            }
            //  if (file != null)
            // {
            // HttpPostedFileBase file = Request.Files[0];
                if (file != null && file.ContentLength > 0)
            {
                string ext = Path.GetExtension(file.FileName).ToLower();
                if (file.ContentLength > 2100000)
                {
                    ModelState.AddModelError("StaffImg", "File size is too big..! max file size is allowed is approx 2mb");
                }
                if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg")
                {
                    string myFullFileName = Guid.NewGuid() + Path.GetFileName(file.FileName);
                    model.StaffImg = "Images/StaffImages/" + myFullFileName;
                    myFullFileName = "~/Images/StaffImages/" + myFullFileName;
                    file.SaveAs(Server.MapPath(myFullFileName));
                }
                else
                {
                    ModelState.AddModelError("StaffImg", "Only supports jpg, png, gif, jpeg formats ..!");
                }
            }
            //  }

            model.DropDownList = this._staffRepo.GetStaffDetailMasterDropDownList(model.CorrCountry, model.CorrState,
                  model.PermCountry, model.PermState);

            //if (model.Function == "SAVE")
            //{
            //    model.CorrCountry = System.Configuration.ConfigurationManager.AppSettings["CountryID"].ToString();
            //    model.CorrState = System.Configuration.ConfigurationManager.AppSettings["StateID"].ToString();
            //    model.CorrCity = System.Configuration.ConfigurationManager.AppSettings["CityID"].ToString();
            //    model.DropDownList = this._staffRepo.GetStaffDetailMasterDropDownList(model.CorrCountry, model.CorrState,
            //        model.PermCountry, model.PermState);
            //}
            //else
            //{
            //    model.DropDownList = this._staffRepo.GetStaffDetailMasterDropDownList(model.CorrCountry, model.CorrState,
            //        model.PermCountry, model.PermState);
            //}

            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                Tuple<int, string> res = _staffRepo.Staff_StaffDetailMaster_CRUD(model);
                ViewBag.msg = res.Item2;
                ViewBag.color = res.Item1 == 1 ? "Success" : "Warning";
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
                //return  RedirectToAction(viewName);
            }
            var errors = from modelstate in ModelState.AsEnumerable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsEnumerable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
            //ViewBag.msg = errors.Count() > 0 ? "Something is going wrong/missing, please check again..!" : "Failed to Save/Update, please try again later or contact or administrator";
            //ViewBag.color = errors.Count() > 0 ? "Info" : "Warning";


            //return RedirectToAction(viewName);
        }

        [HttpPost]
        public ActionResult Staff_StaffDetailMaster_CRUD_SELF(Staff_StaffDetailMasterModel model)
        {
            string viewName = "SelfUpdation";
            model.DropDownList = this._staffRepo.GetStaffDetailMasterDropDownList(model.CorrCountry, model.CorrState,model.PermCountry, model.PermState);
            if (ModelState.IsValid)
            {
                model.UserId = Session["UserId"].ToString();
                model.Function = "SELFUPDATE";
                Tuple<int, string> res = _staffRepo.Staff_StaffDetailMaster_CRUD(model);
                ViewBag.msg = res.Item2;
                ViewBag.color = res.Item1 == 1 ? "Success" : "Warning";
//                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
                return RedirectToAction(viewName);
            }
            var errors = from modelstate in ModelState.AsEnumerable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            ViewBag.msg = errors.Count() > 0 ? "Something is going wrong/missing, please check again..!" : "Failed to Save/Update, please try again later or contact or administrator";
            ViewBag.color = errors.Count() > 0 ? "Info" : "Warning";
            return RedirectToAction(viewName);
        }
        //[EncryptedActionParameter]
        public PartialViewResult _StaffTimeTable(string StaffId, string StaffName)
        {
            TimeTable_SetupModels model = _staffRepo.GetStaffTimeTable(StaffId);
            if (model != null)
                model.ClassTeacherName = StaffName;
            return PartialView(model);
        }


        public ActionResult SelfUpdation()
        {
            string staffId =  _staffRepo.GetStaffIDByUserID(Session["UserId"].ToString());
            Staff_StaffDetailMasterModel model = this._staffRepo.GetStaff_StaffDetailMasterByStaffID(staffId);
            model.Function = "UPDATE";
            ///model.staffDocumentList = _staffDocRepo.Staff_DocumentUpload_Transaction(staffId);
            model.DropDownList = this._staffRepo.GetStaffDetailMasterDropDownList(model.CorrCountry, model.CorrState,
            model.PermCountry, model.PermState);
            return View(model);
        }
    }
}
