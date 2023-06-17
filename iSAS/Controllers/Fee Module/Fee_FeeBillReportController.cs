using ISas.Entities.FeesEntities;
using ISas.Repository.Academic.IRepository;
using ISas.Repository.FeeModuleRepo.IRepository;
using ISas.Repository.Interface;
using ISas.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ISas.Web.Controllers.Fee_Module
{
    [Authorize]
    [ExceptionHandler]
    public class Fee_FeeBillReportController : Controller
    {
        private IFee_FeeBillReportRepo _feeBillReportRepo;
        private IAcademic_ClassSetupRepo _classRepo;
        private ICommonRepo _commonRepo;
        public Fee_FeeBillReportController(IFee_FeeBillReportRepo feeBillReportRepo
            , IAcademic_ClassSetupRepo classRepo, ICommonRepo commonRepo)
        {
            _feeBillReportRepo = feeBillReportRepo;
            _classRepo = classRepo;
            _commonRepo = commonRepo;
        }

        // GET: Fee_FeeBillReport
        public ViewResult Fee_FeeBillReport_FilterReport()
        {
            return View(new Fee_FeeBillReportModels { ClassWithSectionList = _classRepo.All_ClassWithSectionList_DropDown(Session["UserId"].ToString()) });
        }

        public PartialViewResult _FeeBillReport_StudentDetails(string ClassSecId, string StrectureId)
        {
            Fee_FeeBillReportModels model = new Fee_FeeBillReportModels();
            model.StudentList = _commonRepo.Basic_StudentInformation_ByMode("ClassSec_StructId", Session["SessionId"].ToString(),
                ClassSecId, StrectureId, null, null, null);
            return PartialView(model);
        }


        public ViewResult Fee_FeeBillReport_Html(Fee_FeeBillReportModels param)
        {
            param.SessionId = Session["SessionId"].ToString();
            if (string.IsNullOrEmpty(param.Selected_ERPNos))
                param.Selected_ERPNos = string.Join(",", param.StudentList.Where(r => r.Selected).Select(r => r.ERP).ToList());

            return View(_feeBillReportRepo.Fee_BillReportDetails(param));
        }
    }
}