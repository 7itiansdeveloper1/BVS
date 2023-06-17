using ISas.Entities.Academic;
using ISas.Repository.Academic.IRepository;
using ISas.Web.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Academic
{
    [Authorize]
    [ExceptionHandler]
    public class Academic_SchoolSetupController : Controller
    {
        private IAcademic_SchoolSetupRepo _schoolSetupRepo;
        public Academic_SchoolSetupController(IAcademic_SchoolSetupRepo schoolSetup)
        {
            _schoolSetupRepo = schoolSetup;
        }

        // GET: Fee_FeeHeadMaster
        public ActionResult LandingPage()
        {
            return PartialView(_schoolSetupRepo.GetSchoolList(0));
        }

        public ActionResult New()
        {
            Academic_SchoolSetupModels model = new Academic_SchoolSetupModels();
            //Session["sessLogo"] = null;
            model.Logo = "Images/System/defaultLogo.png";
            return View(model);
        }

        //[HttpPost]
        //public JsonResult UploadFile(string id)
        //{
        //    if (Request.Files.Count > 0)
        //    {
        //        HttpPostedFileBase file = Request.Files[0];
        //        if (file != null && file.ContentLength > 0)
        //        {
        //            string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
        //            if (file.ContentLength > 2100000)
        //            {
        //                return Json("File size is too big..! max file size is allowed is approx 2mb", JsonRequestBehavior.AllowGet);
        //            }
        //            if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg")
        //            {
        //                Session["sessLogo"] = file;
        //                //myFullFileName = Path.GetFileName(file.FileName);
        //                //myFullFileName = Guid.NewGuid() + myFullFileName;
        //                //mySavetoFileName = myFullFileName;
        //                // myFullFileName = "~/Images/StudentIamges/" + myFullFileName;
        //                //file.SaveAs(Server.MapPath(myFullFileName));
        //                return Json("", JsonRequestBehavior.AllowGet);
        //            }
        //            else
        //            {
        //                return Json("Only supports jpg, png, gif, jpeg formats ..!", JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //    }
        //    //string result = UploadStudentPhoto_CRUD(StudentERPNo, mySavetoFileName, ForDocType);
        //    return Json("No image found..!", JsonRequestBehavior.AllowGet);
        //}

        [EncryptedActionParameter]
        public ActionResult Updation(string SchoolId)
        {
            //Session["sessLogo"] = null;
            return View(_schoolSetupRepo.GetSchoolDetailsById(Convert.ToInt32(SchoolId)));
        }

        [HttpPost]
        public JsonResult Academic_SchoolSetup_CRUD(Academic_SchoolSetupModels model)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                        if (file.ContentLength > 2100000)
                        {
                            ModelState.AddModelError("Logo", "File size is too big..! max file size is allowed is approx 2mb");
                        }
                        if (ext == ".jpg" || ext == ".png" || ext == ".gif" || ext == ".jpeg")
                        {
                            string myFullFileName = Guid.NewGuid() + Path.GetFileName(file.FileName);
                            model.Logo = "Images/System/" + myFullFileName;
                            myFullFileName = "~/Images/System/" + myFullFileName;
                            file.SaveAs(Server.MapPath(myFullFileName));
                        }
                        else
                        {
                            ModelState.AddModelError("Logo", "Only supports jpg, png, gif, jpeg formats ..!");
                        }
                    }
                }
            }
            if (ModelState.IsValid)
            {
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _schoolSetupRepo.Academic_SchoolSetup_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Academic_SchoolSetup_Delete(int SchoolId)
        {
            Tuple<int, string> res = _schoolSetupRepo.Academic_SchoolSetup_CRUD(SchoolId);
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}