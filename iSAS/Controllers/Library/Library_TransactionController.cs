using ISas.Entities;
using ISas.Entities.LibraryEntities;
using ISas.Repository.Library.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Library
{
    [Authorize]
    [ExceptionHandler]
    public class Library_TransactionController : Controller
    {
        private ILibrary_TransactionRepo _libRepo;
        private ILibrary_SetupRepo _libMstRepo;
        public Library_TransactionController(ILibrary_TransactionRepo libRepo, ILibrary_SetupRepo libMst)
        {
            _libRepo = libRepo;
            _libMstRepo = libMst;
        }

        // GET: Library_Transaction
        public ActionResult Library_TransactionMainPage()
        {
            Session["IssueBookDetails"] = null;
            Library_TransactionModels model = new Library_TransactionModels();
            model.TransDate = DateTime.Now.ToShortDateString().Replace("-", "/");
            model.LibraryList = _libMstRepo.GetLibraryList("").Select(r => new SelectListItem
            {
                Text = r.LibName,
                Value = r.LibId,
            }).ToList();
            model.BookIssueDetails.IssueDays = Convert.ToInt32(ConfigurationManager.AppSettings["BookIssueDays"].ToString());
            return View(model);
        }

        #region Book Issue

        public PartialViewResult _Library_Transaction_BookIssue()
        {
            return PartialView();
        }

        public PartialViewResult _BookIssueDetials(Library_Transaction_BookDetailsModels bookDetails)
        {
            Library_TransactionModels model = null;
            

            if (Session["IssueBookDetails"] != null)
               model = Session["IssueBookDetails"] as Library_TransactionModels;

            if (model == null)
            model = new Library_TransactionModels();

            if (model.BookIssueDetails.BookDetails.Where(r => r.AccNo.ToUpper().Replace(" ", "") == bookDetails.AccNo.ToUpper().Replace(" ", "")).Count() > 0)
            {
                int indexNo = model.BookIssueDetails.BookDetails.FindIndex(r => r.AccNo.ToUpper().Replace(" ", "") == bookDetails.AccNo.ToUpper().Replace(" ", ""));
                model.BookIssueDetails.BookDetails[indexNo].IssuedDays = bookDetails.IssuedDays;
            }
            else
            {
                model.BookIssueDetails.BookDetails.Add(bookDetails);
            }
            Session["IssueBookDetails"] = model;
            return PartialView(model);
        }




        #endregion


        #region Book Return

        public PartialViewResult _Library_Transaction_BookReturn()
        {
            return PartialView();
        }

        public PartialViewResult _BookReturnDetials(string LibId, string ERPNo, string ReturnDate)
        {
            return PartialView(_libRepo.GetReturnBookDetails(LibId, ERPNo, ReturnDate));
        }


        #endregion


        public JsonResult GetBookDetails(string AccNo, string LibID)
        {
            return Json(_libRepo.GetBookDetails(AccNo, LibID), JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public JsonResult Library_Transaction_CRUD(Library_TransactionModels model, StudentSearchModel studentdetails)
        {
            model.UserId = Session["UserID"].ToString();
            model.StudentDetails = studentdetails;
            Tuple<int, string> res = _libRepo.Library_Transaction_CRUD(model);
            Session["IssueBookDetails"] = null;
            return Json(new { status = "success", Msg = res.Item2, Color = res.Item1 == 1 ? "Success" : "Warning" }, JsonRequestBehavior.AllowGet);
        }
    }
}