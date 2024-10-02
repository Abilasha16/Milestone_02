using System.ComponentModel.DataAnnotations;

namespace Trail_Milestone2.Entity
{
    public class MotorBike
    {
        [Key]
        public Guid MotorbikeId { get; set; }
        public string RegisterNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public bool AvailabilityStatus { get; set; }
        

    }
}
