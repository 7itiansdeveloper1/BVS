using System;
using System.Linq;
using System.Web.Mvc;
using WebMatrix.WebData;
using ISas.Web.Models;
using System.Data;
using ISas.Repository.Interface;
using ISas.Repository.StaffRepository.IRepository;

namespace ISas.Web.Controllers
{
    [Authorize]
    [ExceptionHandler]
    public class AccountController : Controller
    {
        private IStaff_StaffDetailMasterRepo _staffRepo;
        ILoginData objILoginData;
        private ICommonRepo _commonRepo;
        private IStudentSession _studentSessionRepos;
        public string _userName = System.Web.HttpContext.Current.User.Identity.Name.ToString() != null ? System.Web.HttpContext.Current.User.Identity.Name.ToString() : null;
        //public string UserId = System.Web.HttpContext.Current.Session != null ?System.Web.HttpContext.Current.Session["UserID"].ToString() : null;
        public AccountController(ILoginData loginRepos,IStudentSession loginSession, IStaff_StaffDetailMasterRepo staffRepo, ICommonRepo commonRepo)
        {
            objILoginData = loginRepos;
            this._studentSessionRepos = loginSession;
            this._staffRepo = staffRepo;
            _commonRepo = commonRepo;
        }
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            string authType1 = User.Identity.AuthenticationType;
            bool isAuth1 = User.Identity.IsAuthenticated;
            string authName1 = User.Identity.Name;

            var model = new Login();
            //var sessions = objILoginData.GetAllSessions();
            var sessions = this._studentSessionRepos.GetAllSessions();
            model.SessionList = sessions != null && sessions.Any() ? sessions.OrderByDescending(p => p.PrintOrder)
                .Select(p => new SelectListItem
                {
                    Text = p.SessionDisplayName,
                    Value = p.SessId,
                    Selected = p.IsDefault
                }) : Enumerable.Empty<SelectListItem>();

            //string clientWiseLoginEnabled = System.Configuration.ConfigurationManager.AppSettings["ClientWiseLoginEnabled"];
            //if (clientWiseLoginEnabled == "YES")
            //{
            //    string loginPageName = System.Configuration.ConfigurationManager.AppSettings["ClientWise_LoginPageName"];
            //    return View(loginPageName, model);
            //}

            ViewBag.HeadDetails = _commonRepo.ReportHeaderDetails("");
            ViewBag.ClientInfo = _commonRepo.get_ClientInfo();
            

            return View(model);
            //return View();
        }


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Logout()
        {

            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Login");
            //return View();
        }

        [HttpGet]
        public ActionResult Keepalive()
        {
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            var model = new LocalPasswordModel();
            model.UserName = _userName;
            //return RedirectToAction("Login");
            return View(model);
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ChangePassword(LocalPasswordModel loginuser)
        {
            if (ModelState.IsValid)
            {

                //var model = new LocalPasswordModel();
                bool success=WebSecurity.ChangePassword(_userName, loginuser.OldPassword, loginuser.NewPassword);

                if (success == true)
                {
                    return RedirectToAction("Logout");
                }
                else
                {
                    ModelState.AddModelError("OldPassword","The current password is incorrect.");
                    var model = new LocalPasswordModel();
                    model.UserName = _userName;
                    return View(model);
                }
            }
            else
            {
                //ModelState.AddModelError("Error", "Please enter Username and Password");
                var model = new LocalPasswordModel();
                model.UserName = _userName;
                //return RedirectToAction("Login");
                return View(model);
                //return View(login);
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                bool success = WebSecurity.Login(login.username, login.password, false);
                if (success == true)
                {
                    Tuple<string, string, string,string> UserIDs = objILoginData.GetUserID_By_UserName(login.username);
                    var LoginType = objILoginData.GetRoleByUserID(UserIDs.Item1);
                    var SessId = login.SelectedSessionId; //objILoginData.GetAllSessions().OrderByDescending(m => m.SessId).First().SessId;
                    var SessName = objILoginData.GetAllSessions().Where(r => r.SessId == SessId).FirstOrDefault().SessionDisplayName; //objILoginData.GetAllSessions().OrderByDescending(m => m.SessId).First().SessionDisplayName;
                    if (string.IsNullOrEmpty(Convert.ToString(LoginType)))
                    {
                        ModelState.AddModelError("Error", "Rights to User are not Provide Contact to Admin");
                        return View(login);
                    }
                    else
                    {
                        Session["Name"] = login.username;
                        Session["UserID"] = UserIDs.Item1;
                        Session["LoginType"] = LoginType;
                        Session["SessionId"] = SessId;
                        Session["DisplayName"] = UserIDs.Item2;
                        Session["DisplayImage"] = UserIDs.Item3;
                        Session["SessionName"] = SessName;
                        Session["LoginStudentERPNo"] = UserIDs.Item1;
                        Session["DisplayTitle"] = UserIDs.Item4;
                        return RedirectToAction("Dashboard", "Dashboard");
                    }
                }
                else
                {
                    ModelState.AddModelError("Error", "Username / Password is not correct...!!!");
                    var model = new Login();
                    //var sessions = objILoginData.GetAllSessions();
                    var sessions = this._studentSessionRepos.GetAllSessions();
                    model.SessionList = sessions != null && sessions.Any() ? sessions.OrderByDescending(p => p.PrintOrder)
                        .Select(p => new SelectListItem
                        {
                            Text = p.SessionDisplayName,
                            Value = p.SessId,
                            Selected = p.IsDefault
                        }) : Enumerable.Empty<SelectListItem>();

                    ViewBag.HeadDetails = _commonRepo.ReportHeaderDetails("");
                    ViewBag.ClientInfo = _commonRepo.get_ClientInfo();

                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Entered username or credentials are not correct...!!!");
                var model = new Login();
                //var sessions = objILoginData.GetAllSessions();
                var sessions = this._studentSessionRepos.GetAllSessions();
                model.SessionList = sessions != null && sessions.Any() ? sessions.OrderByDescending(p => p.PrintOrder)
                    .Select(p => new SelectListItem
                    {
                        Text = p.SessionDisplayName,
                        Value = p.SessId,
                        Selected = p.IsDefault
                    }) : Enumerable.Empty<SelectListItem>();
                ViewBag.HeadDetails = _commonRepo.ReportHeaderDetails("");
                ViewBag.ClientInfo = _commonRepo.get_ClientInfo();


                return View(model);
            }
        }

        public ActionResult EditUserProfile()
        {
            if (User.IsInRole("Student"))
            {
                return RedirectToAction("StudentProfile", "Dashboard");
            }else
            {
                //string userID = Session["UserID"] == null ? "" : Session["UserID"].ToString();
                //string StaffID = this._staffRepo.GetStaffIDByUserID(userID);
                //Staff_StaffDetailMasterModel model = this._staffRepo.GetStaff_StaffDetailMasterByStaffID(StaffID);
                //model.Function = "UPDATE";
                //model.DropDownList = this._staffRepo.GetStaffDetailMasterDropDownList(model.CorrCountry, model.CorrState,
                //    model.PermCountry, model.PermState);
                return View(_staffRepo.GetStaffProfileDetails(Session["UserID"].ToString()));
            }
        }
    }
}
