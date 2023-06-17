using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.Exam_Entities
{
    public class Exam_DownloadResultModels
    {
        public int Sno { get; set; }
        public string  SessionId { get; set; }
        public string ClassId { get; set; }
        public string SectionId { get; set; }
        public string ExamId { get; set; }
        public string ERPNo { get; set; }
        public string Student { get; set; }
        public string ExamName { get; set; }
    }
}
