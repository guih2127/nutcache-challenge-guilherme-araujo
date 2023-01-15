using EmployeesAPI.Domain;
using EmployeesAPI.Extensions;
using EmployeesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI.Controllers
{
    [Route("/api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IEnumerable<EmployeeModel>> GetAllAsync()
        {
            var employees = await _employeeService.ListAsync();
            return employees;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveEmployeeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _employeeService.SaveAsync(model);

            if (!result.Success)
                return StatusCode((int)result.StatusCode, result.Message);

            return StatusCode((int)result.StatusCode, result.Employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] SaveEmployeeModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var result = await _employeeService.UpdateAsync(id, model);

            if (!result.Success)
                return StatusCode((int)result.StatusCode, result.Message);

            return StatusCode((int)result.StatusCode, result.Employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _employeeService.DeleteAsync(id);

            if (!result.Success)
                return StatusCode((int)result.StatusCode, result.Message);

            return StatusCode((int)result.StatusCode, result.Employee);
        }
    }
}
