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
        //Bike Available
        public async Task<bool> IsBikeAvailable(Guid motorbikeid)
        {
            using(var connection =new SqlConnection(_connectionstring))
            {
                await connection.OpenAsync();
                var cmd = new SqlCommand("SELECT AvailabilityStatus FROM Motorbike WHERE MotorbikeId = @motorbikeid)",connection);
                cmd.Parameters.AddWithValue("@motorbikeid",motorbikeid);

                var availabilitystatus = await cmd.ExecuteScalarAsync();
                return availabilitystatus == "Available";
            }
        }


        //Rent bike

        public async Task<Rental> RentBike(Rental rental)
        {
            using(var connection = new SqlConnection(_connectionstring))
            {
                var cmd = new SqlCommand("INSERT INTO Rental(RentalId,MotorbikeId,CustomerId,RentalDate,ReturnDate,OverdueStatus,RentalStatus) " +
                    "VALUES (@rentalid,@bikeid,@customerid,@rentaldate,@returndate,@overdue,@rentalstatus);UPDATE Motorbike AvailabilityStatus = 'Unavailable' where MotorbikeId = @motorbikeid", connection);

                cmd.Parameters.AddWithValue("@rentalid", rental.RentalId);
                cmd.Parameters.AddWithValue("@bikeid", rental.MotorbikeId);
                cmd.Parameters.AddWithValue("@customerid", rental.CustomerId);
                cmd.Parameters.AddWithValue("@rentaldate", rental.RentalDate);
                cmd.Parameters.AddWithValue("@returndate",(object) rental.ReturnDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@overdue", rental.OverdueStatus);
                cmd.Parameters.AddWithValue("@rentalstatus", rental.RentalStatus);

                await cmd.ExecuteNonQueryAsync();
            }
            return rental;
        }
    }
}
