using ISas.Entities.FeesEntities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Web.Models;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_ReceiptHeaderMasterController : Controller
    {
        private IFee_ReceiptHeaderMasterRepo _feeheaderMst;
        public Fee_ReceiptHeaderMasterController(IFee_ReceiptHeaderMasterRepo feeheaderMst)
        {
            _feeheaderMst = feeheaderMst;
        }
        // GET: Fee_ReceiptHeaderMaster
        public ActionResult LandingPage()
        {
            return View(_feeheaderMst.GetFee_ReceiptHeaderMasterList(""));
        }

        public ActionResult New()
        {
            Fee_ReceiptHeaderMasterModels model = new Fee_ReceiptHeaderMasterModels();
            model.Logo = "Images/System/defaultLogo.png";
            return View(model);
        }

        [EncryptedActionParameter]
        public ActionResult Updation(string HeaderId)
        {
            return View(_feeheaderMst.Fee_ReceiptHeaderMasterById(HeaderId));
        }

        [HttpPost]
        public ActionResult Fee_ReceiptHeaderMaster_CRUD(Fee_ReceiptHeaderMasterModels model)
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
                if (model.IsEnableStr == "on")
                    model.IsEnable = true;

                model.UserId = Session["UserID"].ToString();
                model.CRUDMode = "SAVE";
                Tuple<int, string> res = _feeheaderMst.Fee_ReceiptHeaderMaster_CRUD(model);
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }

            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }


        #region Wing Header Setup
        public ViewResult New_WingHeaderSetup()
        {
            return View(_feeheaderMst.WingHeaderSetupDetailsList(null));
        }
        public PartialViewResult _Update_WingHeaderSetup(string WingId)
        {
            return PartialView(_feeheaderMst.WingHeaderSetupDetails(WingId));
        }


        [HttpPost]
        public ActionResult Fee_WingHeaderSetup_CRUD(Fee_WingHeaderSetupModel model)
        {
            string previousLogoString = model.Logo;
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
               // model.UserId = Session["UserID"].ToString();
                model.CRUDMode = "UPDATE";
                Tuple<int, string> res = _feeheaderMst.Fee_WingHeaderSetup_CRUD(model);
                if(Request.Files.Count > 0 && !string.IsNullOrEmpty(previousLogoString) && res.Item1 == 1)
                {
                    if (System.IO.File.Exists(Server.MapPath("~/" + previousLogoString)))
                    {
                        System.IO.File.Delete(Server.MapPath("~/" + previousLogoString));
                    }
                }
                return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Fee_WingHeaderSetup_RemoveHeader(string WingId, string Logo)
        {
            Tuple<int, string> res = _feeheaderMst.Fee_WingHeaderSetup_CRUD(WingId);
            if (!string.IsNullOrEmpty(Logo) && res.Item1 == 1)
                if (System.IO.File.Exists(Server.MapPath("~/" + Logo)))
                    System.IO.File.Delete(Server.MapPath("~/" + Logo));

            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}