using Microsoft.AspNetCore.Mvc;
using PatientInformation.Models.ViewModels;

namespace PatientInformation.Repository.Interface
{
    public interface IDiseaseRepository
    {
        Task<VmResponseMessage> CreateDisease(VmRequestModel vm);
        Task<VmResponseMessage> EditDisease(VmRequestModel vm);
        Task<List<VmRequestModel>> GetAllDisease();
        Task<VmRequestModel> GetDiseaseById(int id);
        Task<List<VmSelectListItem>> GetAllDiseaseForDropdown();
        List<VmSelectListItem> GetEpilepsyDropdown();
        Task<bool> RemoveDisease(int id);
    }
}
