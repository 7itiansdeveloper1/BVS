using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISas.Entities.Exam_Entities;
using ISas.Repository.ExamRepository.IRepository;
using ISas.Repository.ExamRepository.Repository;

namespace ISas.Web.Controllers.Exam_Module
{
    [Authorize]
    public class Exam_DownloadResultController : Controller
    {
        private IExam_DownloadResultRepo _downloadResultRepo=new Exam_DownloadResultRepo();
        public ActionResult GetResult()
        {
            List<Exam_DownloadResultModels> downloadResulList = new List<Exam_DownloadResultModels>();
            downloadResulList = _downloadResultRepo.DownloadResult(Session["UserId"].ToString(), Session["SessionId"].ToString());
            return View(downloadResulList);
        }
    }
}