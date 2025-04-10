using EmployeeDetils_Mangement.Repository;
using EmployeeDetils_Mangement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetils_Mangement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpPost("Add-Update-Employee-Detail")]
        public async Task<ActionResult> AddUpdateEmployeeDetail([FromQuery] InsertUpdateEmployeeDetail employeeDetail)
        {
            if (employeeDetail == null)
            {
                return BadRequest("Invalid employee details.");
            }
            var result = await _employeeRepository.AddUpdateEmployeeDetail(employeeDetail);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("Get-all-Employee-Detail")]
        public async Task<ActionResult> GetEmployeeDetail()
        {
            var result = await _employeeRepository.GetEmployeeDetail();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("Get-Employee-Detail-By-Id/{Id}")]
        public async Task<ActionResult> GetEmployeeDetailById(int Id)
        {
            var result = await _employeeRepository.GetEmployeeDetailById(Id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpDelete("Delete-Employee-Detail-By-Id/{Id}")]
        public async Task<ActionResult> DeleteEmployeeDetailById(int Id)
        {
            var result = await _employeeRepository.DeleteEmployeeDetailById(Id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
