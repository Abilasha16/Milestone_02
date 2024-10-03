using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trail_Milestone2.DTO.Reguest;
using Trail_Milestone2.IService;

namespace Trail_Milestone2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Customer_PageController : ControllerBase
    {
        private readonly ICustomer_PageService _service;

        public Customer_PageController(ICustomer_PageService service)
        {
            _service = service;
        }

        [HttpPost("RegisterCustomer")]
        public async Task<IActionResult>RegisterCustomer(CustomerReguest customerReguest)
        {
            if (customerReguest == null)
            {
                return BadRequest("Invalid Customer Data");
            }
            var data = await _service.CustomerRegister(customerReguest);
            return Ok(data);
        }
    }
}
