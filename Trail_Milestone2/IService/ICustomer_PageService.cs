using Trail_Milestone2.DTO.Reguest;
using Trail_Milestone2.DTO.Response;

namespace Trail_Milestone2.IService
{
    public interface ICustomer_PageService
    {
        Task<CustomerResponse> CustomerRegister(CustomerReguest customerReguest);
    }
}
