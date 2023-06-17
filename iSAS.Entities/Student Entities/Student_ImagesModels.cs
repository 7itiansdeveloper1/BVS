using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISas.Entities.Student_Entities
{
    public class Student_ImagesModels
    {
        public string ERPNo { get; set; }
        public string StudentImageURL { get; set; }
        public string FatherImageURL { get; set; }
        public string MotherImageURL { get; set; }
        public string GuardianImageURL { get; set; }
        public string GrandFatherImageURL { get; set; }
        public string GrandMotherImageURL { get; set; }
        public string OtherImageURL { get; set; }

        public string StudentImageURL_Croped { get; set; }
        public string FatherImageURL_Croped { get; set; }
        public string MotherImageURL_Croped { get; set; }
        public string GuardianImageURL_Croped { get; set; }
        public string GrandFatherImageURL_Croped { get; set; }
        public string GrandMotherImageURL_Croped { get; set; }
        public string OtherImageURL_Croped { get; set; }
        public string UploadFor { get; set; }
        public string ImageForName { get; set; }
    }
}
