using Trail_Milestone2.Entity;

namespace Trail_Milestone2.IRepo
{
    public interface ICustomer_PageRepo
    {
        Task<Customer> CustomerRegister(Customer customer);
    }
}
