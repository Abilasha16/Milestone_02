using Trail_Milestone2.Entity;

namespace Trail_Milestone2.DTO.Response
{
    public class MotorbikeResponse
    {
        public Guid MotorbikeId { get; set; }
        public string RegisterNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public bool AvailabilityStatus { get; set; }

    }
}
