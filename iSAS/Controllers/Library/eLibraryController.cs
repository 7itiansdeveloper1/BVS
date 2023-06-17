using ISas.Entities.CommonEntities;
using ISas.Entities.LibraryEntities;
using ISas.Repository.Interface;
using ISas.Repository.Library.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Library
{
    [Authorize]
    [ExceptionHandler]
    public class eLibraryController : Controller
    {
        private IeLibraryRepo _eLibraryRepo;
        public eLibraryController(IeLibraryRepo eLibraryRepo)
        {
            this._eLibraryRepo = eLibraryRepo;

        }
        
        public ActionResult EBookLanding()
        {
            return View(this._eLibraryRepo.eLibrary_Transaction(null,0,"Landing"));
        }
        public PartialViewResult _SearchResult(string classid = null, string ebookNo = "0")
        {
            return PartialView(this._eLibraryRepo.eLibrary_Transaction(classid, Convert.ToInt32(ebookNo),"Landing"));
        }

        public ActionResult NeweBook()
        {
            return View(_eLibraryRepo.eLibrary_Transaction(null,0,"SAVE"));
        }
        
        [EncryptedActionParameter]
        public ActionResult EditeBook(string eBookNo)
        {
            return View(_eLibraryRepo.eLibrary_Transaction(Convert.ToInt32(eBookNo),"UPDATE"));
        }


        [HttpPost]
        public JsonResult eBook_CRUD(eLibraryModels model)
        {
            string myFullFileName = "";
            eLibraryBook book = new eLibraryBook();
            if (ModelState.IsValid)
            {
                Common_AttachemntRefrence attachment = new Common_AttachemntRefrence();
                attachment.filePath = "Images/EBOOK/";
                Tuple<int, string> attachmetStatus = new Tuple<int, string>(1, null);
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    if (file != null && file.ContentLength > 0)
                    {
                        string ext = Path.GetExtension(file.FileName).ToLower();
                        if (file.ContentLength > 4200000)
                        {
                            return Json(new { Msg = "File size is too big..! max file size is allowed is approx 4mb", Color = "Warning" }, JsonRequestBehavior.AllowGet);
                        }
                        if (ext == ".pdf")
                        {
                            myFullFileName = Path.GetFileName(file.FileName);
                            myFullFileName = Guid.NewGuid() +"_"+ myFullFileName;
                            myFullFileName = "/" + attachment.filePath + myFullFileName;
                            file.SaveAs(Server.MapPath(myFullFileName));
                            attachment.fileName = file.FileName;
                            attachment.filePath = myFullFileName;
                        }
                        else
                        {
                            return Json(new { status = "success", Msg = "Only supports jpg, png, gif, jpeg formats ..!", Color = "Warning" }, JsonRequestBehavior.AllowGet);
                        }
                    }
                    
                    book.eBookattachementName = file.FileName;
                    book.eBookattachementPath = myFullFileName;
                }
            }
            book.eBookNo = model.eLibraryBook.eBookNo;
            book.eBookName = model.eLibraryBook.eBookName;
            book.eBookClassId = model.eLibraryBook.eBookClassId;
            book.eBookSubjectId = model.eLibraryBook.eBookSubjectId;
            book.eBookType = model.eLibraryBook.eBookType;
            book.isActive = model.eLibraryBook.isActive;
            book.remark = model.eLibraryBook.remark;
            if (ModelState.IsValid)
            {
                if (model.Function == "UPDATE")
                {
                    Tuple<int, string> res = _eLibraryRepo.eLibrary_CRUD(book, model.Function, Session["UserId"].ToString());
                    return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
                }
                else if (model.Function=="SAVE" && myFullFileName!="")
                {
                    Tuple<int, string> res = _eLibraryRepo.eLibrary_CRUD(book, model.Function, Session["UserId"].ToString());
                    return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = "success", Msg = "Please upload the E-Book before save..!", Color = "Warning" }, JsonRequestBehavior.AllowGet);
                }
                
            }
            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete_eBook(string bookNo,string oldFilePath)
        {
            eLibraryBook model = new eLibraryBook();
            model.eBookNo = Convert.ToInt32(bookNo);
            FileInfo fileToDelete = new FileInfo(Server.MapPath("~" + oldFilePath));
            if (fileToDelete.Exists)
                fileToDelete.Delete();
            Tuple<int, string> res = _eLibraryRepo.eLibrary_CRUD(model,"DELETE",Session["UserId"].ToString());
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}