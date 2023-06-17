using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.DashboardEntities
{
    public class OnlineClassesModel
    {
        public OnlineClassesModel()
        {
            List<onlineclassStudent> _onlineclassesStudentList = new List<onlineclassStudent>();
        }
        public string secretCode { get; set; }
        public List<onlineclassStudent> _onlineclassesStudentList { get; set;}
        
    }
    public class onlineclassStudent
    {
        public Guid id { get; set; }
        public string onlineClassName { get; set; }
        public string ClasName { get; set; }
        public string subjectName { get; set; }
        public string OnlineClassStartTime { get; set; }
        public bool IsclassAvaialbe { get; set; }
        public string zoomURL { get; set; }
    }
}
