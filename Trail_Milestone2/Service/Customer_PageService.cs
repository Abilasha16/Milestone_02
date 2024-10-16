using Trail_Milestone2.DTO.Reguest;
using Trail_Milestone2.DTO.Response;
using Trail_Milestone2.Entity;
using Trail_Milestone2.IRepo;
using Trail_Milestone2.IService;

namespace Trail_Milestone2.Service
{
    public class Customer_PageService : ICustomer_PageService
    {
        private readonly ICustomer_PageRepo _repo;

        public Customer_PageService(ICustomer_PageRepo repo)
        {
            _repo = repo;
        }

        public async Task<CustomerResponse>CustomerRegister(CustomerReguest customerReguest)
        {
            var data = new Customer()
            {
                CustomerId = Guid.NewGuid(),
                FullName = customerReguest.FullName,
                NIC = customerReguest.NIC,
                Email = customerReguest.Email,
                LicenseNumber = customerReguest.LicenseNumber,
                PhoneNumber = customerReguest.PhoneNumber,
            };

            var registercustomer = await _repo.CustomerRegister(data);
            var res = new CustomerResponse()
            {
                CustomerId = data.CustomerId,
                FullName = data.FullName,
                NIC = data.NIC,
                Email = data.Email,
                LicenseNumber = data.LicenseNumber,
                PhoneNumber = data.PhoneNumber,
            };
            return res;
        }

        public async Task<RentalResponse> RentBike(RentalRequest rentalRequest)
        {
            bool isAvailable = await _repo.IsBikeAvailable(rentalRequest.MotorbikeId);
            if (!isAvailable)
            {
                throw new Exception("Bike is Already Rented");
            }

            var data = new Rental()
            {
                RentalId = Guid.NewGuid(),
                MotorbikeId = rentalRequest.MotorbikeId,
                CustomerId = rentalRequest.CustomerId,
                RentalDate = DateTime.Now,
                ReturnDate = rentalRequest.ReturnDate,
                OverdueStatus = false,  // Set initial overdue status to false
                RentalStatus = rentalRequest.RentalStatus,
            };
            await _repo.RentBike(data);

            var res = new RentalResponse()
            {
                RentalId = data.RentalId,
                MotorbikeId = data.MotorbikeId,
                CustomerId = data.CustomerId,
                RentalDate = data.RentalDate,
                ReturnDate = data.ReturnDate,
                OverdueStatus = data.OverdueStatus,
                RentalStatus = data.RentalStatus,
            };

            return res;
        }


        //public async Task<string> RentBike(RentalRequest rentalRequest)
        //{
        //    // Check if the motorbike is available
        //    bool isAvailable = await _repo.IsBikeAvailable(rentalRequest.MotorbikeId);
        //    if (!isAvailable)
        //    {
        //        return "Motorbike is not available for rent.";
        //    }

        //    // Create rental object
        //    var rental = new Rental
        //    {
        //        RentalId = Guid.NewGuid(),
        //        MotorbikeId = rentalRequest.MotorbikeId,
        //        CustomerId = rentalRequest.CustomerId,
        //        RentalDate = rentalRequest.RentalDate,
        //        ReturnDate = rentalRequest.ReturnDate,
        //        OverdueStatus = rentalRequest.OverdueStatus,
        //        RentalStatus = rentalRequest.RentalStatus
        //    };

        //    // Rent the bike
        //    await _repo.RentBike(rental);

        //    return "Bike rented successfully.";
        //}

       
        public async Task UpdateOverdueRentals()
        {
            // Step 1: Get rentals that are overdue (more than 24 hours old and OverdueStatus is still false)
            var overdueRentals = await _repo.GetRentalsToBeMarkedOverdue();

            // Step 2: Loop through each rental and update its OverdueStatus to true
            foreach (var rental in overdueRentals)
            {
                rental.OverdueStatus = true; // Mark the rental as overdue

                // Step 3: Update the rental in the database
                await _repo.UpdateRentalOverdueStatus(rental);
            }
        }

    }
}
