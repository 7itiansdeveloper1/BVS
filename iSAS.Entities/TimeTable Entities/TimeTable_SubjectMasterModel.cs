using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.TimeTable_Entities
{
    public class TimeTable_SubjectMasterModel
    {
        public TimeTable_SubjectMasterModel()
        {
            timeTableSubject = new timetable_subject();
            timetablesubjectList = new List<timetable_subject>();
        }

        public timetable_subject timeTableSubject { get; set; }
        public List<timetable_subject> timetablesubjectList { get; set;}
    }

    public class timetable_subject{

        public string SubjectId { get; set; }
        [Required(ErrorMessage = "Subject Name is Req..!")]
        [Display(Name = "Subject Name")]
        public string SubjectName { get; set; }

        [Required(ErrorMessage = "Print Order is Req..!")]
        [Display(Name = "Print Order")]
        public int PrintOrder { get; set; }

        [Display(Name = "Is Active ?")]
        public bool IsActive { get; set; }

        public string userId { get; set; }

        [Display(Name = "Grouping Name if any")]
        public string groupName { get; set; }

        public int subjectTeacher { get; set; }

        public int timeTableClass { get; set; }

    }
}
