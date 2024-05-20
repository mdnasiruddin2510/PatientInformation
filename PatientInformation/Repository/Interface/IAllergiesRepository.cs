using PatientInformation.Models.ViewModels;

namespace PatientInformation.Repository.Interface
{
    public interface IAllergiesRepository
    {
        Task<VmResponseMessage> CreateAllergies(VmRequestModel vm);
        Task<VmResponseMessage> EditAllergies(VmRequestModel vm);
        Task<List<VmRequestModel>> GetAllAllergies();
        Task<VmRequestModel> GetAllergiesById(int id);
        Task<bool> RemoveAllergies(int id);
    }
}
