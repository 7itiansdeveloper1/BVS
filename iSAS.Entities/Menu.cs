using System.Collections.Generic;

namespace ISas.Entities
{
    public class Menu
    {
        public Module MainMenu { get; set; }
        public IList<Role> SubMenus { get; set; }
    }
}