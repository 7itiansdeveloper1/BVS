using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace ISas.Web.Models
{
    public class Login
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
        [Required(ErrorMessage = "Enter User name")]
        public string username { get; set; }
        public int userid { get; set; }
        public string SessID { get; set; }
        public string SessName { get; set; }
    }
}
