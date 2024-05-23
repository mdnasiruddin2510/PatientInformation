using PatientInformation.Models.Enum;

namespace PatientInformation.Models.ViewModels
{
    public class VmPatient
    {
        
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DiseaseName { get; set; } = string.Empty;
        public int DiseaseId { get; set; }
        public string EpilepsyDup { get; set; } = string.Empty;
        public Epilepsy Epilepsy { get; set; }
        public List<VmNcdDetails> NCDDetails { get; set; } = new List<VmNcdDetails>();
        public List<VmAllergiesDetails> AllergiesDetails { get; set; } = new List<VmAllergiesDetails>();
    }
    public class VmNcdDetails
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class VmAllergiesDetails
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    
}
