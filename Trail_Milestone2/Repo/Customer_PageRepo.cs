using Microsoft.Data.SqlClient;
using Trail_Milestone2.Entity;
using Trail_Milestone2.IRepo;

namespace Trail_Milestone2.Repo
{
    public class Customer_PageRepo : ICustomer_PageRepo
    {
        private readonly string _connectionstring;

        public Customer_PageRepo(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

        //CustomerRegister

        public async Task<Customer>  CustomerRegister(Customer customer)
        {
            using(var connection = new SqlConnection(_connectionstring))
            {
                var cmd = new SqlCommand("insert into Customer (CustomerId , FullName , NIC , Address, Email , LicenseNumber , PhoneNumber) " +
                    " VALUES (@id,@name,@nic,@address,@email,@licenseno,@phoneno)", connection);


                cmd.Parameters.AddWithValue("@id", customer.CustomerId);
                cmd.Parameters.AddWithValue("@name",customer.FullName);
                cmd.Parameters.AddWithValue("@nic",customer.NIC);
                cmd.Parameters.AddWithValue("@address", customer.Address);

                cmd.Parameters.AddWithValue("@email",customer.Email);
                cmd.Parameters.AddWithValue("@licenseno",customer.LicenseNumber);   
                cmd.Parameters.AddWithValue("@phoneno",customer.PhoneNumber);


                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
            return customer;
            
        }
    }
}
