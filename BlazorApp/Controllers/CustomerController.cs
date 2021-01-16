using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorApp
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerViewModel>>> GetAsync()
        {
            var result = await _customerService.GetCustomersAsync(10, 0);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> PostAsync([FromForm] CustomerViewModel customer)
        {
            var result = await _customerService.CreateCustomerAsync(customer);

            return Created("customer/" + result.Id, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutAsync([FromRoute] string id, [FromForm] CustomerViewModel customer)
        {
            var result = await _customerService.UpdateCustomerAsync(id, customer);

            if (result == null) return BadRequest();
            return Created("customer/" + result.Id, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);

            if (!result) return NotFound();

            return NoContent();
        }
    }
}
