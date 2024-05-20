using Microsoft.AspNetCore.Mvc;
using PatientInformation.Models.ViewModels;
using PatientInformation.Repository.Interface;
using System.Globalization;

namespace PatientInformation.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NCDController : ControllerBase
    {
        private readonly INCDRepository _repository;
        public NCDController(INCDRepository repository)
        {
            _repository = repository;
        }
        [HttpPost("CreateNCD")]
        public async Task<ActionResult<VmResponseMessage>> CreateNCD(VmRequestModel vm)
        {
            var response = await _repository.CreateNCD(vm);
            return Ok(response);
        }
        [HttpPost("EditNCD")]
        public async Task<ActionResult<VmResponseMessage>> EditNCD(VmRequestModel vm)
        {
            var response = await _repository.EditNCD(vm);
            return Ok(response);
        }
        [Route("GetAllNCD")]
        [HttpPost]
        public async Task<ActionResult<List<VmRequestModel>>> GetAllNCD()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;

            var data = await _repository.GetAllNCD();
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x => x.Name.ToLower().Contains(searchValue.ToLower())).ToList();
            }

            data = data.OrderByDescending(o => o.Id).ToList();

            int recordsTotal = data.Count();
            var records = data.Skip(skip).Take(pageSize).ToList();
            return Ok(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = records });
        }
        [HttpGet("GetNCDById")]
        public async Task<ActionResult<VmRequestModel>> GetNCDById(int id)
        {
            var response = await _repository.GetNCDById(id);
            return Ok(response);
        }
        [HttpDelete]
        [Route("RemoveNCD")]
        public async Task<ActionResult<bool>> RemoveNCD(int id)
        {
           var response =  await _repository.RemoveNCD(id);
            return Ok(response);
        }

    }
}
