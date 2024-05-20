using PatientInformation.Models.ViewModels;
using PatientInformation.Models;
using PatientInformation.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatientInformation.Models.Enum;

namespace PatientInformation.Repository
{
    public class DiseaseRepository:IDiseaseRepository
    {
        private readonly AppDbContext _db;
        public DiseaseRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<VmResponseMessage> CreateDisease(VmRequestModel vm)
        {
            var response = new VmResponseMessage();
            var existDisease = await _db.Disease.FirstOrDefaultAsync(x => x.Name.ToLower() == vm.Name.ToLower());
            if (existDisease == null)
            {
                var model = new Disease
                {
                    Name = vm.Name
                };
                await _db.Disease.AddAsync(model);
                await _db.SaveChangesAsync();

                response.Type = "Success";
                response.Message = "Successfully Saved!";
            }
            else
            {
                response.Type = "Error";
                response.Message = "Already Exist!";
            }
            return response;
        }
        public async Task<VmResponseMessage> EditDisease(VmRequestModel vm)
        {
            var response = new VmResponseMessage();
            var exist = await _db.Disease.FirstOrDefaultAsync(x => x.Id == vm.Id);
            if (exist is not null)
            {
                exist.Name = vm.Name;
                _db.Update(exist);
                await _db.SaveChangesAsync();
                response.Type = "Success";
                response.Message = "Successfully Updated!";
            }
            else
            {
                response.Type = "Error";
                response.Message = "Not Found!";
            }
            return response;
        }
        public async Task<List<VmRequestModel>> GetAllDisease()
        {
            var dataList = await _db.Disease
                                    .Select(s => new VmRequestModel
                                    {
                                        Id = s.Id,
                                        Name = s.Name
                                    }).ToListAsync();
            return dataList;
        } 
        public async Task<List<VmSelectListItem>> GetAllDiseaseForDropdown()
        {
            var dataList = await _db.Disease
                                    .Select(s => new VmSelectListItem
                                    {
                                        Value = s.Id.ToString(),
                                        Text = s.Name
                                    }).ToListAsync();
            return dataList;
        }

        public async Task<VmRequestModel> GetDiseaseById(int id)
        {
            var data = await _db.Disease.Where(x => x.Id == id)
                                .Select(s => new VmRequestModel
                                {
                                    Id = s.Id,
                                    Name = s.Name
                                }).FirstOrDefaultAsync();
            return data;
        }

        public List<VmSelectListItem> GetEpilepsyDropdown()
        {
            return Enum.GetValues(typeof(Epilepsy)).Cast<Epilepsy>().Select(v => new VmSelectListItem { Text = v.ToString(), Value = v.ToString() }).ToList();
        }

        public async Task<bool> RemoveDisease(int id)
        {
            var response = false;
            var data = await _db.Disease.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                _db.Disease.Remove(data);
                await _db.SaveChangesAsync();
                response = true;
            }
            else
            {
                response = false;
            }
            return response;
        }
    }
}
