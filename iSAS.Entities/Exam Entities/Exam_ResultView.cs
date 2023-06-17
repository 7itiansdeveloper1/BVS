using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities
{
    class Exam_ResultView
    {

    }
    public class ResultViewModel
    {
        private IEnumerable<SelectListItem> _sessionList;
        private IEnumerable<SelectListItem> _examList;
        public ResultViewModel()
        {
            SessionList = new List<SelectListItem>();
            ExamList = new List<SelectListItem>();
        }

        public string SelectedSessionId { get; set; }
        public string SelectedExamId { get; set; }
        public string UserId { get; set; }


        public IEnumerable<SelectListItem> SessionList
        {
            get
            {
                return this._sessionList;
            }
            set
            {
                this._sessionList = value;
            }

        }

        public IEnumerable<SelectListItem> ExamList
        {
            get
            {
                return this._examList;
            }
            set
            {
                this._examList = value;
            }
        }



    }
    public class StudentGradeCardViewModel
    {
        public StudentGradeCardViewModel()
        {
            HeaderNameList = new List<string>();
            ValueList = new List<List<string>>();
        }
        public List<List<string>> ValueList { get; set; }
        public List<string> HeaderNameList { get; set; }
        public string sessionId { get; set; }
        public string classId { get; set; }
        public string sectionId { get; set; }
        public string examId { get; set; }
        public string erpno { get; set; }
        public string studentname { get; set; }
        public string SelectedExamName { get; set; }
        public bool IsDownloadable { get; set; }
    }
}
