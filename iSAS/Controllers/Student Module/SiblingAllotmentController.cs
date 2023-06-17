using ISas.Entities;
using ISas.Entities.Student_Entities;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Repository.Interface;
using ISas.Repository.StudentRegistrationRepository.IRepository;
using ISas.Web.Models;
using System.Linq;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Student_Module
{
    [Authorize]
    [ExceptionHandler]
    public class SiblingAllotmentController : Controller
    {
        private IStudentClass _classRepo;
        private ISiblingAllotmentRepo _sibLingRepo;
        private IFee_TransactionRepo _feeTrnRepo;
        public SiblingAllotmentController(IStudentClass ClassRepo, ISiblingAllotmentRepo siblingRepo,IFee_TransactionRepo feeTrnRepo)
        {
            _classRepo = ClassRepo;
            _sibLingRepo = siblingRepo;
            _feeTrnRepo = feeTrnRepo;
        }


        // GET: SiblingAllotment
        public ActionResult SiblingAllotment_Filter()
        {
            SiblingAllotmentModels model = new SiblingAllotmentModels();
            model.ClassList = _classRepo.GetAllClasses(Session["UserID"].ToString()).Select(r => new SelectListItem
            {
                 Text = r.ClassName,
                 Value = r.ClassId
            }).ToList();
            return View(model);
        }

        public PartialViewResult _SiblingAllotment(string ClassId, string SectionId)
        {
            return PartialView(_sibLingRepo.GetStudentList(ClassId, SectionId, Session["SessionId"].ToString(), Session["UserId"].ToString()));
        }

        //public ActionResult SiblingAllotment_CRUD(SiblingAllotmentModels model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        model.UserId = Session["UserID"].ToString();
        //        string receiptNo = _sibLingRepo.SiblingAllotment_CRUD(model);
        //        return Json(new { status = "success", Msg = "Your receipt no is " + receiptNo, ReceiptNo = receiptNo }, JsonRequestBehavior.AllowGet);
        //    }

        //    var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
        //    var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
        //    return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult SiblingAllotment_CRUD(string selecteERPNo,string siblingERPNo)
        {
            if (ModelState.IsValid)
            {
                SiblingAllotmentModels model = new SiblingAllotmentModels();
                model.UserId = Session["UserID"].ToString();
                model.ERPNo = selecteERPNo;
                model.SiblingIds = siblingERPNo;
                string responseMsg = _sibLingRepo.SiblingAllotment_CRUD(model);
                return Json(new { status = "success", Msg = responseMsg}, JsonRequestBehavior.AllowGet);
            }

            var errors = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count > 0) select new { Title = modelstate.Key, Error = modelstate.Value.Errors.FirstOrDefault().ErrorMessage };
            var validKeys = from modelstate in ModelState.AsQueryable().Where(f => f.Value.Errors.Count == 0) select new { Title = modelstate.Key };
            return Json(new { status = "failed", ErrorList = errors, ValidKeyList = validKeys }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult _StudentSearchByProperty(int selectedRow)
        {
            StudentSearchByPropertyModel model = new StudentSearchByPropertyModel();


            model.ClassList = _classRepo.GetAllClasses().OrderBy(r => r.PrintOrder).Select(r => new SelectListItem
            {
                Text = r.ClassName,
                Value = r.ClassId,
            }).ToList();
            model.selectedRow = selectedRow;
            return PartialView(model);
        }

        public PartialViewResult _PossibleSibling(string studentName, string studentERP)
        {
            return PartialView(_sibLingRepo.GetStudentPossibleSiblingList(studentERP, studentName,Session["SessionId"].ToString()));
        }
        public PartialViewResult _StudentDetail(string SearchType, string SearchText, string ClassID, int SelectedRow)
        {
            ViewBag.SelectedRow = SelectedRow;
            return PartialView(_feeTrnRepo.GetStudentDetails(SearchType, SearchText, ClassID, Session["SessionId"].ToString()));
        }
    }
}