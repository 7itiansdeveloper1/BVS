using ISas.Entities.CommonEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.Student_Entities
{
    public class Student_IdentityCardModel
    {
        public Student_IdentityCardModel()
        {
            HeaderDetails = new ReportHeaderEntities();
            StudentDetails = new List<IdentityCard_StudentDetailsModel>();
        }

        public bool GenAdmitCard { get; set; }
        public string rptname { get; set; }
        public ReportHeaderEntities HeaderDetails { get; set; }
        public List<IdentityCard_StudentDetailsModel> StudentDetails { get; set; }

    }
    public class IdentityCard_StudentDetailsModel
    {
        public bool Selected { get; set; }
        public long sno { get; set; }
        public string RollNo { get; set; }
        public string Session { get; set; }
        public string ERPNo { get; set; }
        public string DOA { get; set; }
        public string AdmNo { get; set; }
        public string Student { get; set; }
        public string Class { get; set; }
        public string Gender { get; set; }
        public string DOB { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string FMobile { get; set; }
        public string MMobile { get; set; }
        public string Route { get; set; }
        public string Stop { get; set; }
        public string Address { get; set; }
        public string S { get; set; }
        public string F { get; set; }
        public string M { get; set; }
        public string BG { get; set; }
    }
}
