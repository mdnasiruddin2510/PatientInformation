using PatientInformation.Models.ViewModels;

namespace PatientInformation.Repository.Interface
{
    public interface IPatientRepository
    {
        Task<VmResponseMessage> CreatePatient(VmPatient vm);
        Task<VmResponseMessage> EditPatient(VmPatient vm);
        Task<List<VmPatient>> GetAllPatient();
        Task<VmPatient> GetPatientById(int id);
        Task<List<VmNcdDetails>> GetNCDByPatientId(int id);
        Task<List<VmAllergiesDetails>> GetAllergiesByPatientId(int id);
        Task<bool> RemovePatient(int id);
    }
}
