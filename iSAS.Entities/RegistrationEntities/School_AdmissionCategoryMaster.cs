namespace ISas.Entities.RegistrationEntities
{
    public class School_AdmissionCategoryMaster
    {
        public string AdmCategoryId { get; set; }
        public string AdmCategoryName { get; set; }
        public bool IsDefault { get; set; }
        public int  PrintOrder { get; set; }
    }
}
