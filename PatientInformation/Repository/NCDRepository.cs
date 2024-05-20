using Azure;
using Microsoft.EntityFrameworkCore;
using PatientInformation.Models;
using PatientInformation.Models.ViewModels;
using PatientInformation.Repository.Interface;

namespace PatientInformation.Repository
{
    public class NCDRepository : INCDRepository
    {
        private readonly AppDbContext _db;
        public NCDRepository(AppDbContext db) 
        {
            _db = db;
        }
        public async Task<VmResponseMessage> CreateNCD(VmRequestModel vm)
        {
            var response = new VmResponseMessage();
            var existNCD = await _db.NCD.FirstOrDefaultAsync(x => x.Name.ToLower() == vm.Name.ToLower());
            if (existNCD == null)
            {
                var model = new NCD
                {
                    Name = vm.Name
                };
                await _db.NCD.AddAsync(model);
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
        public async Task<VmResponseMessage> EditNCD(VmRequestModel vm)
        {
            var response = new VmResponseMessage();  
            var exist = await _db.NCD.FirstOrDefaultAsync(x => x.Id == vm.Id);
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
        public async Task<List<VmRequestModel>> GetAllNCD()
        {
           var dataList = await _db.NCD
                                   .Select(s => new VmRequestModel
                                   {
                                       Id = s.Id,
                                       Name = s.Name
                                   }).ToListAsync();
            return dataList;
        }

        public async Task<VmRequestModel> GetNCDById(int id)
        {
            var data = await _db.NCD.Where(x => x.Id == id)
                                .Select(s => new VmRequestModel
                                {
                                    Id= s.Id,
                                    Name = s.Name
                                }).FirstOrDefaultAsync();
            return data;
        }
        public async Task<bool> RemoveNCD(int id)
        {
            var response = false;
            var data = await _db.NCD.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                _db.NCD.Remove(data);
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
