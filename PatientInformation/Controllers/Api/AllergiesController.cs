using Microsoft.AspNetCore.Mvc;
using PatientInformation.Models.ViewModels;
using PatientInformation.Repository.Interface;

namespace PatientInformation.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AllergiesController : ControllerBase
    {
        private readonly IAllergiesRepository _repository;
        public AllergiesController(IAllergiesRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("CreateAllergies")]
        public async Task<ActionResult<VmResponseMessage>> CreateAllergies(VmRequestModel vm)
        {
            var response = await _repository.CreateAllergies(vm);
            return Ok(response);
        }
        [HttpPost("EditAllergies")]
        public async Task<ActionResult<VmResponseMessage>> EditAllergies(VmRequestModel vm)
        {
            var response = await _repository.EditAllergies(vm);
            return Ok(response);
        }
        [Route("GetAllAllergies")]
        [HttpPost]
        public async Task<ActionResult<List<VmRequestModel>>> GetAllAllergies()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var data = await _repository.GetAllAllergies();
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Name.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            data = data.OrderByDescending(o => o.Id).ToList();

            int recordsTotal = data.Count();
            var records = data.Skip(skip).Take(pageSize).ToList();
            return Ok(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = records });
        }
        [HttpGet("GetAllergiesById")]
        public async Task<ActionResult<VmRequestModel>> GetAllergiesById(int id)
        {
            var response = await _repository.GetAllergiesById(id);
            return Ok(response);
        }
        [HttpDelete]
        [Route("RemoveAllergies")]
        public async Task<ActionResult<bool>> RemoveAllergies(int id)
        {
            var response = await _repository.RemoveAllergies(id);
            return Ok(response);
        }
    }
}
