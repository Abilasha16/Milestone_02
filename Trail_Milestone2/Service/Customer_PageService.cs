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
                Address = customerReguest.Address,
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
                Address = data.Address,
                LicenseNumber = data.LicenseNumber,
                PhoneNumber = data.PhoneNumber,
            };
            return res;
        }
    }
}
