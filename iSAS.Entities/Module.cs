using System;

namespace ISas.Entities
{
    public class Module : IEquatable<Module>
    {
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public int DisplayOrder { get; set; }
        public byte[] ModuleIcon { get; set; }

        public string IconName { get; set; }

        public bool Equals(Module other)
        {
            //Check whether the compared object is null. 
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data. 
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the products' properties are equal. 
            return ModuleId.Equals(other.ModuleId);
        }

        // If Equals() returns true for a pair of objects  
        // then GetHashCode() must return the same value for these objects. 
        public override int GetHashCode()
        {
            //Get hash code for the Code field. 
            return ModuleId.GetHashCode();
        }
    }
}
