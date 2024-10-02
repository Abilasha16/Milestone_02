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
    }
}
