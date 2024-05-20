using PatientInformation.Models.ViewModels;

namespace PatientInformation.Repository.Interface
{
    public interface INCDRepository
    {
        Task<VmResponseMessage> CreateNCD(VmRequestModel vm);
        Task<VmResponseMessage> EditNCD(VmRequestModel vm);
        Task<List<VmRequestModel>> GetAllNCD();
        Task<VmRequestModel> GetNCDById(int id);
        Task<bool> RemoveNCD(int id);
    }
}
