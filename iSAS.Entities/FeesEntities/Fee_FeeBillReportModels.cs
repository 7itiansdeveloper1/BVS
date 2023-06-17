using ISas.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.FeesEntities
{
    public class Fee_FeeBillReportModels
    {
        public Fee_FeeBillReportModels()
        {
            StudentList = new List<Basic_StudentInfoModel>();
            ClassWithSectionList = new List<SelectListItem>();
            StrectureList = new List<SelectListItem>();
            InstallmentList = new List<SelectListItem>();
        }

        public string Selected_ERPNos { get; set; }
        public string ClassSectionId { get; set; }
        public string StrectureId { get; set; }
        public string InstallmentId { get; set; }
        public string SessionId { get; set; }

        public List<Basic_StudentInfoModel> StudentList { get; set; }

        public List<SelectListItem> ClassWithSectionList { get; set; }
        public List<SelectListItem> StrectureList { get; set; }
        public List<SelectListItem> InstallmentList { get; set; }
    }

    public class Fee_FeeBillReportHtmlModel
    {
        public Fee_FeeBillReportHtmlModel()
        {
            ReportHeader = new ReportHeaderEntities();
            StudentList = new List<Basic_StudentInfoModel>();
            HeadDetails = new List<HeadDetailsModel>();
        }

        public ReportHeaderEntities ReportHeader { get; set; }
        public List<Basic_StudentInfoModel> StudentList { get; set; }
        public List<HeadDetailsModel> HeadDetails { get; set; }
    }
}
