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
    }
}
