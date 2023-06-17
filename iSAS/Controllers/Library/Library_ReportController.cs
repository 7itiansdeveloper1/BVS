using ISas.Entities.LibraryEntities;
using ISas.Repository.Interface;
using ISas.Repository.Library.IRepository;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Library
{
    [Authorize]
    [ExceptionHandler]
    public class Library_ReportController : Controller
    {
        private ILibrary_ReportRepo _libraryReportRepo;
        private ICommonRepo _commonRepo;
        public Library_ReportController(ILibrary_ReportRepo libraryReportRepo, ICommonRepo commonRepo)
        {
            _libraryReportRepo = libraryReportRepo;
            _commonRepo = commonRepo;
        }

        // GET: Student_Report
        public ActionResult LibraryReport_Filter()
        {
            return View(_libraryReportRepo.Library_Transaction_GetReportList());
        }

        
        public ActionResult _LibraryDetailReport(string reportId, string fromDate, string toDate)
        {
            
            return PartialView(_libraryReportRepo.Library_Transaction_GetReport( Convert.ToInt32(reportId), fromDate, toDate, Session["SessionID"].ToString()));
        }

    }

}