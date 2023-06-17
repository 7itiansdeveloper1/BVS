
using System.Collections.Generic;

namespace ISas.Entities
{
    public class StudentRemarkTable
    {
        public string StudentERPNo { get; set; }
        public string RemarkId { get; set; }
        
    }
    //public class OptionalSubjectParametersList
    //{
    //    public string FieldName { get; set; }
    //    public string FieldDisplayName { get; set; }
    //}
    public class StudentRemarkList
    {
        public StudentRemarkList()
        {
            PreviousSelectedRemarksIds = new List<string>();
        }
        public Student Student { get; set; }
        public string RemarkId { get; set; }
        public List<string> PreviousSelectedRemarksIds { get; set; } // Not to be selected current

    }
    public class RemarkList
    {
        public string RemarkId { get; set; }
        public string RemarkText { get; set; }
        public int PrintOrder { get; set; }
    }
}
