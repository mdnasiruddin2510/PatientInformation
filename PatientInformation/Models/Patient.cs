using PatientInformation.Models.Enum;

namespace PatientInformation.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DiseaseId { get; set; }
        public Epilepsy Epilepsy { get; set; }
    }
}
