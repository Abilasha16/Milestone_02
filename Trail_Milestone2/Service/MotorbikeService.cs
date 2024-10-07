using Azure.Core;
using Trail_Milestone2.DTO.Reguest;
using Trail_Milestone2.DTO.Response;
using Trail_Milestone2.Entity;
using Trail_Milestone2.IRepo;
using Trail_Milestone2.IService;

namespace Trail_Milestone2.Service
{
    public class MotorbikeService : IMotorbikeService
    {
        private readonly IMotorbikeRepo _motorbikeRepo;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MotorbikeService(IMotorbikeRepo motorbikeRepo, IWebHostEnvironment webHost)
        {
            _motorbikeRepo = motorbikeRepo;
            _webHostEnvironment = webHost ?? throw new ArgumentNullException(nameof(webHost));
        }
        //public async Task<MotorbikeResponse> AddBike(MotorbikeReguest motorbikeReguest/*, IFormFile imageFile*/)
        //{
        //    Console.WriteLine($"WebRootPath: {_webHostEnvironment.WebRootPath}");

        //    // Check if WebRootPath is null
        //    if (string.IsNullOrEmpty(_webHostEnvironment.WebRootPath))
        //    {
        //        throw new InvalidOperationException("WebRootPath is not set.");
        //    }


        //    var uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads"); if (!Directory.Exists(uploadsDir))
        //    {
        //        Directory.CreateDirectory(uploadsDir);
        //    }

        //    var uniqueFileName = Guid.NewGuid().ToString() + "_" + motorbikeReguest.ImageUrl.FileName;
        //    var filePath = Path.Combine(uploadsDir, uniqueFileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await motorbikeReguest.ImageUrl.CopyToAsync(stream);
        //    }


        //    var data = new MotorBike()
        //    {
        //        MotorbikeId = Guid.NewGuid(),
        //        RegisterNumber = motorbikeReguest.RegisterNumber,
        //        Brand = motorbikeReguest.Brand,
        //        Model = motorbikeReguest.Model,
        //        Category = motorbikeReguest.Category,
        //        ImageUrl = $"/uploads/{uniqueFileName}",
        //        AvailabilityStatus = motorbikeReguest.AvailabilityStatus,

        //    };
        //     var addbike = await _motorbikeRepo.AddBike(data);

        //    var res = new MotorbikeResponse();
        //    res.MotorbikeId = addbike.MotorbikeId;
        //    res.RegisterNumber = addbike.RegisterNumber;
        //    res.Brand = addbike.Brand;
        //    res.Model = addbike.Model;
        //    res.Category = addbike.Category;
        //    res.ImageUrl = addbike.ImageUrl;
        //    res.AvailabilityStatus = addbike.AvailabilityStatus;

        //    return res;

        //}


        public async Task<MotorbikeResponse> AddBike(MotorbikeReguest motorbikeReguest)
        {
            // register number check

            var registercheck = await _motorbikeRepo.GetRegisterNumber(motorbikeReguest.RegisterNumber);
            if (registercheck != null)
            {
                throw new InvalidOperationException("Already a Motorbike this Register number.");
            }

            if (string.IsNullOrEmpty(_webHostEnvironment.WebRootPath))
            {
                throw new InvalidOperationException("WebRootPath is not set.");
            }

            var uploadsDir = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsDir))
            {
                Directory.CreateDirectory(uploadsDir);
            }

            var imageUrls = new List<string>();

            // Loop through each image file
            foreach (var imageFile in motorbikeReguest.ImageUrls)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                var filePath = Path.Combine(uploadsDir, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                imageUrls.Add($"/uploads/{uniqueFileName}");
            }

            var data = new MotorBike()
            {
                MotorbikeId = Guid.NewGuid(),
                RegisterNumber = motorbikeReguest.RegisterNumber,
                Brand = motorbikeReguest.Brand,
                Model = motorbikeReguest.Model,
                Category = motorbikeReguest.Category,
                ImageUrl = string.Join(",", imageUrls),  // Save as comma-separated string
                AvailabilityStatus = motorbikeReguest.AvailabilityStatus,
            };

            var addbike = await _motorbikeRepo.AddBike(data);

            var res = new MotorbikeResponse()
            {
                MotorbikeId = addbike.MotorbikeId,
                RegisterNumber = addbike.RegisterNumber,
                Brand = addbike.Brand,
                Model = addbike.Model,
                Category = addbike.Category,
                ImageUrl = addbike.ImageUrl,
                AvailabilityStatus = addbike.AvailabilityStatus,
            };

            return res;
        }


        public async Task<MotorbikeResponse> EditBike(Guid id, MotorbikeReguest motorbikeReguest)
        {
            var existingBike = await _motorbikeRepo.EditBike(id);

            if (existingBike == null)
            {
                return null;
            }
            existingBike.RegisterNumber = motorbikeReguest.RegisterNumber;
            existingBike.Brand = motorbikeReguest.Brand;
            existingBike.Model = motorbikeReguest.Model;
            existingBike.Category = motorbikeReguest.Category;
            //existingBike.ImageUrl = motorbikeReguest.ImageUrl;
            existingBike.AvailabilityStatus = motorbikeReguest.AvailabilityStatus;

            var updatebike = await _motorbikeRepo.UpdateBike(existingBike);

            if (updatebike == null)
            {
                return null;
            }

            var res = new MotorbikeResponse()
            {
                MotorbikeId = updatebike.MotorbikeId,
                RegisterNumber = updatebike.RegisterNumber,
                Brand = updatebike.Brand,
                Model = updatebike.Model,
                Category = updatebike.Category,
                //ImageUrl = updatebike.ImageUrl,
                AvailabilityStatus = updatebike.AvailabilityStatus,
            };
            return res;
        }

        public async Task<List<MotorbikeResponse>> GetAllBike()
        {
            var data = await _motorbikeRepo.GetAllBike();

            var response = new List<MotorbikeResponse>();
            foreach (var bike in data)
            {
                var res = new MotorbikeResponse()
                {
                    MotorbikeId = bike.MotorbikeId,
                    RegisterNumber = bike.RegisterNumber,
                    Brand = bike.Brand,
                    Model = bike.Model,
                    Category = bike.Category,
                    ImageUrl = bike.ImageUrl,
                    AvailabilityStatus = bike.AvailabilityStatus
                };
                response.Add(res);
            }
            return response;
        }

        public async Task<MotorbikeResponse> DeleteBike(Guid id)
        {
            var data = await _motorbikeRepo.DeleteBike(id);
            var response = new MotorbikeResponse()
            {
                MotorbikeId = data.MotorbikeId,
                RegisterNumber = data.RegisterNumber,
                Brand = data.Brand,
                Model = data.Model,
                Category = data.Category,
                AvailabilityStatus = data.AvailabilityStatus
            };
            return response;
        }

        public async Task<MotorbikeResponse> GetById(Guid id)
        {
            var data = await _motorbikeRepo.GetById(id);
            var response = new MotorbikeResponse()
            {
                MotorbikeId = data.MotorbikeId,
                RegisterNumber = data.RegisterNumber,
                Brand = data.Brand,
                Model = data.Model,
                Category = data.Category,
                ImageUrl = data.ImageUrl,
                AvailabilityStatus = data.AvailabilityStatus
            };
            return response;

        }

        public async Task<List<MotorbikeResponse>> Get6Bike()
        {
            var data = await _motorbikeRepo.Get6Bikes();

            var response = new List<MotorbikeResponse>();
            foreach (var bike in data)
            {
                var res = new MotorbikeResponse()
                {
                    MotorbikeId = bike.MotorbikeId,
                    RegisterNumber = bike.RegisterNumber,
                    Brand = bike.Brand,
                    Model = bike.Model,
                    Category = bike.Category,
                    ImageUrl = bike.ImageUrl,
                    AvailabilityStatus = bike.AvailabilityStatus
                };
                response.Add(res);
            }
            return response;
        }
    }


}
