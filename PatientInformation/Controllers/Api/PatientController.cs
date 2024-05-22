using Microsoft.AspNetCore.Mvc;
using PatientInformation.Models.ViewModels;
using PatientInformation.Repository.Interface;

namespace PatientInformation.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _repository;
        public PatientController(IPatientRepository patientRepository)
        {
                _repository = patientRepository;
        }
        [HttpPost("CreatePatient")]
        public async Task<ActionResult<VmResponseMessage>> CreatePatient(VmRequestModel vm)
        {
            var response = await _repository.CreatePatient(vm);
            return Ok(response);
        }
        [HttpPost("EditPatient")]
        public async Task<ActionResult<VmResponseMessage>> EditPatient(VmRequestModel vm)
        {
            var response = await _repository.EditPatient(vm);
            return Ok(response);
        }
        [Route("GetAllPatient")]
        [HttpPost]
        public async Task<ActionResult<List<VmRequestModel>>> GetAllPatient()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var data = await _repository.GetAllPatient();
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Name.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            data = data.OrderByDescending(o => o.Id).ToList();

            int recordsTotal = data.Count();
            var records = data.Skip(skip).Take(pageSize).ToList();
            return Ok(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = records });
        }
        [HttpGet("GetPatientById")]
        public async Task<ActionResult<VmRequestModel>> GetPatientById(int id)
        {
            var response = await _repository.GetPatientById(id);
            return Ok(response);
        }
        [HttpDelete]
        [Route("RemovePatient")]
        public async Task<ActionResult<bool>> RemovePatient(int id)
        {
            var response = await _repository.RemovePatient(id);
            return Ok(response);
        }
    }
}
