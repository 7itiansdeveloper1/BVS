using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ISas.Entities.TimeTable_Entities
{
    public class TimeTable_SetupModels
    {
        public TimeTable_SetupModels()
        {
            SubjectList = new List<TimeTableSubjectListModel>();
            DaysList = new List<string>();
            PeriodDetailsList = new List<TimeTable_PeriodModel>();
            TeacherList = new List<SelectListItem>();
            singleperiodtimetable = new List<SinglePeriodTimeTable>();

        }

        public string ClassSectionId { get; set; }
        public string ClassSectionName { get; set; }
        public string ClassTeacherName { get; set; }


        public string selectedStaffSubjectId { get; set; }

        public string SubjectId { get; set; }

        public string UserId { get; set; }

        public List<TimeTableSubjectListModel> SubjectList { get; set; }
        public List<SelectListItem> TeacherList { get; set; }
        public List<string> DaysList { get; set; }
        public List<TimeTable_PeriodModel> PeriodDetailsList { get; set; }
        public List<SinglePeriodTimeTable> singleperiodtimetable { get; set; }

        public string p { get; set; }
        public string d { get; set; }
    }

    public class TimeTableSubjectListModel
    {
        public string Value { get;set; }
        public string Text { get; set; }
        public string StaffName { get; set; }
        public string StaffId { get; set; }
    }

    public class TimeTable_PeriodModel
    {
        public TimeTable_PeriodModel()
        {
            PeriodInfoList = new List<TimeTable_InfoModel>();
        }

        public string PeriodName { get; set; }
        public string PeriodTime { get; set; }
        public List<TimeTable_InfoModel> PeriodInfoList { get; set; }
    }

    public class SinglePeriodTimeTable
    {
        public SinglePeriodTimeTable()
        {
            dropdownList = new List<singledropDown>();
        }
        public string PeriodName { get; set; }
        public string PeriodTime { get; set; }
        
        public List<singledropDown> dropdownList { get; set; }

    }

    public class singledropDown
    {
        public singledropDown()
        {
            dropdown = new List<SelectListItem>();
        }
        public string teacherSubjectCode { get; set; }
        public List<SelectListItem> dropdown { get; set; }
    }



    public class TimeTable_InfoModel
    {
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
    }




    public class TimeTable_ConfigurationModels
    {
        public string ClassSectionId { get; set; }
        public string ClassName { get; set; }
        public string ClassTeacher { get; set; }
        public string Matrix { get; set; }
    }
}
