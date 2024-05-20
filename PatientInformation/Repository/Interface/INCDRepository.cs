using PatientInformation.Models.ViewModels;

namespace PatientInformation.Repository.Interface
{
    public interface INCDRepository
    {
        Task<VmResponseMessage> CreateNCD(VmRequestModel vm);
    }
}
