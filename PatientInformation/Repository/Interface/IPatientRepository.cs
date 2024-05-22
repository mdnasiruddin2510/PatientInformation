using PatientInformation.Models.ViewModels;

namespace PatientInformation.Repository.Interface
{
    public interface IPatientRepository
    {
        Task<VmResponseMessage> CreatePatient(VmRequestModel vm);
        Task<VmResponseMessage> EditPatient(VmRequestModel vm);
        Task<List<VmRequestModel>> GetAllPatient();
        Task<VmRequestModel> GetPatientById(int id);
        Task<bool> RemovePatient(int id);
    }
}
