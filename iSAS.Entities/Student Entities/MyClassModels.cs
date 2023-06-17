using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.Student_Entities
{
    public class MyClassModels
    {
        public MyClassModels()
        {
            List<MyClass> myClasseList = new List<MyClass>();
        }
        public List<MyClass> myClassList { get; set; }
        public string status { get; set; }
    }
    public class MyClass
    {
        public string AdmNo { get; set; }
        public string DOA { get; set; }
        public string DOB { get; set; }
        public int RollNo { get; set; }
        public string Student { get; set; }
        public string Gender { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string ERP { get; set; }
        public string ContactNo { get; set; }
    }

}
