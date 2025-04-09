using EmployeeDetils_Mangement.Repository;
using EmployeeDetils_Mangement.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeDetils_Mangement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeSalaryDetailController : ControllerBase
    {
        private readonly IEmployeeSalaryDetailRepository _employeeSalaryDetailRepository;
        public EmployeeSalaryDetailController(IEmployeeSalaryDetailRepository employeeSalaryDetailRepository)
        {
            _employeeSalaryDetailRepository = employeeSalaryDetailRepository;
        }

        [HttpPost("Add-Update-Employee-Salary-Detail")]
        public async Task<ActionResult> AddUpdateEmployeeSalaryDetail([FromBody] InsertUpdateEmployeeSalaryDetail employeeSalaryDetail)
        {
            if (employeeSalaryDetail == null)
            {
                return BadRequest("Invalid employee salary details.");
            }
            var result = await _employeeSalaryDetailRepository.AddUpdateEmployeeSalaryDetail(employeeSalaryDetail);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
