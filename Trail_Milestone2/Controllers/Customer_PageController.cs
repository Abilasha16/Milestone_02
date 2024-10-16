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

        [HttpPost("RentBike")]
        public async Task<IActionResult> RentBike(RentalRequest rentalRequest)
        {
            try
            {
                var data = await _service.RentBike(rentalRequest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("OverdueRentals")]
        public IActionResult GetAndMarkOverdueRentals()
        {
            var overdueRentals = _service.GetAndMarkOverdueRentals();
            if (overdueRentals == null || !overdueRentals.Any())
            {
                return NotFound("No overdue rentals found.");
            }
            return Ok(overdueRentals);
        }

    }
}
