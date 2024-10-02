using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trail_Milestone2.DTO.Reguest;
using Trail_Milestone2.IService;

namespace Trail_Milestone2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotorbikeController : ControllerBase
    {
        private readonly IMotorbikeService _motorbikeService;

        public MotorbikeController(IMotorbikeService motorbikeService)
        {
            _motorbikeService = motorbikeService;
        }

        [HttpPost("AddBike")]
        public async Task<IActionResult> AddBike(MotorbikeReguest motorbikeReguest)
        {
            if (motorbikeReguest == null)
            {
                return BadRequest("Invalid motorbike data");
            }

            try
            {
                var data = await _motorbikeService.AddBike(motorbikeReguest);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("EditBike")]

        public async Task<IActionResult> EditBike(Guid id , MotorbikeReguest bikeReguest)
        {
            var data = await _motorbikeService.EditBike(id, bikeReguest);
            return Ok(data);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllBike()
        {
            var data = await _motorbikeService.GetAllBike();
            return Ok(data);
        }

        [HttpDelete("DeleteBike")]
        public async Task<IActionResult> DeleteBike(Guid id)
        {
            var data = await _motorbikeService.DeleteBike(id);
            return Ok(data);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var data = await _motorbikeService.GetById(id);
            return Ok(data);
        }

        [HttpGet("Get6Bikes")]
        public async Task<IActionResult> Get6Bikes()
        {
            var data = await _motorbikeService.Get6Bike();
            return Ok(data);
        }
    }
}
