using Trail_Milestone2.Entity;

namespace Trail_Milestone2.DTO.Reguest
{
    public class MotorbikeReguest
    {
        public string RegisterNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Category { get; set; }
        //public IFormFile ImageUrl { get; set; }

        public List<IFormFile> ImageUrls { get; set; }  // List of images
        public bool AvailabilityStatus { get; set; }


    }
}
