using Microsoft.AspNetCore.Mvc;
using PatientInformation.Models.ViewModels;
using PatientInformation.Repository.Interface;

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
    }
}
