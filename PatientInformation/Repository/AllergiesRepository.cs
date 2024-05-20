using PatientInformation.Models.ViewModels;
using PatientInformation.Models;
using PatientInformation.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace PatientInformation.Repository
{
    public class AllergiesRepository:IAllergiesRepository
    {
        private readonly AppDbContext _db;
        public AllergiesRepository(AppDbContext db)
        {
            _db = db;
        }
        public async Task<VmResponseMessage> CreateAllergies(VmRequestModel vm)
        {
            var response = new VmResponseMessage();
            var existAllergies = await _db.Allergies.FirstOrDefaultAsync(x => x.Name.ToLower() == vm.Name.ToLower());
            if (existAllergies == null)
            {
                var model = new Allergies
                {
                    Name = vm.Name
                };
                await _db.Allergies.AddAsync(model);
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
        public async Task<VmResponseMessage> EditAllergies(VmRequestModel vm)
        {
            var response = new VmResponseMessage();
            var exist = await _db.Allergies.FirstOrDefaultAsync(x => x.Id == vm.Id);
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
        public async Task<List<VmRequestModel>> GetAllAllergies()
        {
            var dataList = await _db.Allergies
                                    .Select(s => new VmRequestModel
                                    {
                                        Id = s.Id,
                                        Name = s.Name
                                    }).ToListAsync();
            return dataList;
        }

        public async Task<VmRequestModel> GetAllergiesById(int id)
        {
            var data = await _db.Allergies.Where(x => x.Id == id)
                                .Select(s => new VmRequestModel
                                {
                                    Id = s.Id,
                                    Name = s.Name
                                }).FirstOrDefaultAsync();
            return data;
        }
        public async Task<bool> RemoveAllergies(int id)
        {
            var response = false;
            var data = await _db.Allergies.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                _db.Allergies.Remove(data);
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
