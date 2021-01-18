using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorApp
{
    [Authorize]
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
        public async Task<ActionResult<CustomerCollection>> GetAsync([FromQuery] PaginationDTO pagination)
        {
            var result = await _customerService.GetCustomersAsync(pagination);

            return Ok(JsonConvert.SerializeObject(result));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerViewModel>> PostAsync([FromForm] CustomerViewModel customer)
        {
            var result = await _customerService.CreateCustomerAsync(customer);

            return Created("customer/" + result.Id, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> PutAsync([FromRoute] Guid id, [FromForm] CustomerViewModel customer)
        {
            var result = await _customerService.UpdateCustomerAsync(id, customer);

            if (result == null) return BadRequest();
            return Created("customer/" + result.Id, result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);

            if (!result) return NotFound();

            return NoContent();
        }
    }
}
