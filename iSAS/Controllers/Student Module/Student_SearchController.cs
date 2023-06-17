using ISas.Entities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Student_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Student_SearchController : Controller
    {
        private IFee_TransactionRepo _feeTrnRepo;
        private IStudentClass _classRepo;


        public Student_SearchController(IFee_TransactionRepo feeTrnRepo, IStudentClass studClassRepo )
        {
            _feeTrnRepo = feeTrnRepo;
            _classRepo = studClassRepo;
        }

        public ActionResult Student_Search()
        {
            StudentSearchByPropertyModel model = new StudentSearchByPropertyModel();
            model.ClassList = _classRepo.GetAllClasses().OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
            {
                Text = r.ClassName,
                Value = r.ClassId,
            }).ToList();
            return View(model);
        }

        public PartialViewResult _SearchResult(string SearchType, string SearchText, string ClassID, int SelectedRow)
        {
            ViewBag.SelectedRow = SelectedRow;
            return PartialView(_feeTrnRepo.GetStudentDetails(SearchType, SearchText, ClassID, Session["SessionId"].ToString()));
        }

    }
}