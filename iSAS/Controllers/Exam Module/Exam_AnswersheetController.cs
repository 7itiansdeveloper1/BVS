using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ISas.Entities.Exam_Entities;
using ISas.Repository.ExamRepository.IRepository;
using ISas.Web.Models;

namespace ISas.Web.Controllers.Exam_Module
{
    public class Exam_AnswersheetController : Controller
    {

        public IExam_AnswersheetRepo _answersheetRepo;
        public Exam_AnswersheetController (IExam_AnswersheetRepo answersheetRepo)
        {
            _answersheetRepo = answersheetRepo;
        }
        [EncryptedActionParameter]
        public ActionResult Answersheet(string qpId)
        {
            StudentAssessmentResult studentAssessmentResult = new StudentAssessmentResult();
            studentAssessmentResult = _answersheetRepo.GetAnswersheet(Session["UserId"].ToString(),Convert.ToInt32(qpId));
            return View(studentAssessmentResult);
        }
    }
}