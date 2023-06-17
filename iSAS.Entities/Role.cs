using System;

namespace ISas.Entities
{
    public class Role
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public string DisplayRoleName { get; set; }
        public Module Module { get; set; }
        public bool Active { get; set; }
        public int PrintOrder { get; set; }
        public string CreatedBy { get; set; }
        public DateTime  CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }

        public string Parameter { get; set; }
    }    
}
