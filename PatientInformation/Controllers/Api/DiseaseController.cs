using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PatientInformation.Models.ViewModels;
using PatientInformation.Repository.Interface;

namespace PatientInformation.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseaseController : ControllerBase
    {
        private readonly IDiseaseRepository _repository;
        public DiseaseController(IDiseaseRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("CreateDisease")]
        public async Task<ActionResult<VmResponseMessage>> CreateDisease(VmRequestModel vm)
        {
            var response = await _repository.CreateDisease(vm);
            return Ok(response);
        }
        [HttpPost("EditDisease")]
        public async Task<ActionResult<VmResponseMessage>> EditDisease(VmRequestModel vm)
        {
            var response = await _repository.EditDisease(vm);
            return Ok(response);
        }
        [Route("GetAllDisease")]
        [HttpPost]
        public async Task<ActionResult<List<VmRequestModel>>> GetAllDisease()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var data = await _repository.GetAllDisease();
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Name.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            data = data.OrderByDescending(o => o.Id).ToList();

            int recordsTotal = data.Count();
            var records = data.Skip(skip).Take(pageSize).ToList();
            return Ok(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = records });
        }
        [HttpGet("GetDiseaseById")]
        public async Task<ActionResult<VmRequestModel>> GetDiseaseById(int id)
        {
            var response = await _repository.GetDiseaseById(id);
            return Ok(response);
        }
        [HttpGet("GetAllDiseaseForDropdown")]
        public async Task<ActionResult<List<VmSelectListItem>>> GetAllDiseaseForDropdown()
        {
            var response = await _repository.GetAllDiseaseForDropdown();
            return Ok(response);
        }
        [HttpDelete]
        [Route("RemoveDisease")]
        public async Task<ActionResult<bool>> RemoveDisease(int id)
        {
            var response = await _repository.RemoveDisease(id);
            return Ok(response);
        }
        [Route("GetEpilepsyDropdown")]
        [HttpGet]
        public ActionResult<List<VmSelectList>> GetEpilepsyDropdown()
        {
            return Ok(_repository.GetEpilepsyDropdown());
        }
    }
}
