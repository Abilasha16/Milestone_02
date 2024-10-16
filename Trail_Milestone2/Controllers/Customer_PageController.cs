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

        [HttpPost("UpdateOverdueRentals")]
        public async Task<IActionResult> UpdateOverdueRentals()
        {
            try
            {
                // Call the service method to update overdue rentals
                await _service.UpdateOverdueRentals();

                return Ok(new { Message = "Overdue rentals updated successfully." });
            }
            catch (Exception ex)
            {
                // Handle any errors and return a BadRequest with the error message
                return BadRequest(new { Error = ex.Message });
            }
        }

    }
}
