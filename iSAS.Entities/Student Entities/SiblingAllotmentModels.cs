using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ISas.Entities.Student_Entities
{
    public class SiblingAllotmentModels
    {
        public SiblingAllotmentModels()
        {
            ClassList = new List<SelectListItem>();
            StudentDetailList = new List<Sibling_StudentDetailModel>();
        }

        [Display(Name = "Class")]
        public string ClassId { get; set; }

        [Display(Name = "Section")]
        public string SectionId { get; set; }


        public string UserId { get; set; }
        public string ERPNo { get; set; }
        public string SiblingIds { get; set; }
        public string studentName { get; set; }

        public List<SelectListItem> ClassList { get; set; }
        public List<Sibling_StudentDetailModel> StudentDetailList { get; set; }
        public List<possibleSibling> possibleSiblingList { get; set; }
    }

    public class Sibling_StudentDetailModel
    {
        public string ERPNo { get; set; }
        public string AdmNo { get; set; }
        public int RollNo { get; set; }
        public string Student { get; set; }
        public string SiblingName { get; set; }
        public bool IsSiblingStudent { get; set; }
    }


    public class possibleSibling
    {
        public bool isSelected { get; set; }
        public string SiblingERP { get; set; }
        public string SiblingName { get; set; }
        public string SiblingClass { get; set; }
    }

}
