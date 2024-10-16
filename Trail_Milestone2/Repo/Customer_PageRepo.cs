﻿using Microsoft.Data.SqlClient;
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
                var cmd = new SqlCommand("insert into Customer (CustomerId , FullName , NIC , Email , LicenseNumber , PhoneNumber) " +
                    " VALUES (@id,@name,@nic,@email,@licenseno,@phoneno)", connection);


                cmd.Parameters.AddWithValue("@id", customer.CustomerId);
                cmd.Parameters.AddWithValue("@name",customer.FullName);
                cmd.Parameters.AddWithValue("@nic",customer.NIC);
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
                var cmd = new SqlCommand("SELECT AvailabilityStatus FROM Motorbike WHERE MotorbikeId = @motorbikeid",connection);
                cmd.Parameters.AddWithValue("@motorbikeid",motorbikeid);

                var availabilitystatus = await cmd.ExecuteScalarAsync();
                return availabilitystatus?.ToString() == "Available";
            }
        }

        //Optional
        public async Task UpdateBikeStatus(Guid motorbikeid, string status)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                await connection.OpenAsync();
                var cmd = new SqlCommand("UPDATE Motorbike SET AvailabilityStatus = @status WHERE MotorbikeId = @motorbikeid", connection);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@motorbikeid", motorbikeid);

                await cmd.ExecuteNonQueryAsync();
            }
        }



        //Rent bike

        public async Task<Rental> RentBike(Rental rental)
        {
           

            using (var connection = new SqlConnection(_connectionstring))
            {
                await connection.OpenAsync();

                var cmd = new SqlCommand("INSERT INTO Rental(RentalId,MotorbikeId,CustomerId,RentalDate,ReturnDate,OverdueStatus,RentalStatus) " +
                    "VALUES (@rentalid,@bikeid,@customerid,@rentaldate,@returndate,@overdue,@rentalstatus)",connection);

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

        //Overdue
        public async Task<List<Rental>> GetRentalsToBeMarkedOverdue()
        {
            var overdueRentals = new List<Rental>();

            using (var connection = new SqlConnection(_connectionstring))
            {
                await connection.OpenAsync();

                var cmd = new SqlCommand("SELECT * FROM Rental WHERE DATEDIFF(HOUR, RentalDate, GETDATE()) >= 24 AND OverdueStatus = 0", connection);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var rental = new Rental
                        {
                            RentalId = reader.GetGuid(0),
                            MotorbikeId = reader.GetGuid(1),
                            CustomerId = reader.GetGuid(2),
                            RentalDate = reader.GetDateTime(3),
                            ReturnDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            OverdueStatus = reader.GetBoolean(5),
                            RentalStatus = reader.GetString(6),
                        };
                        overdueRentals.Add(rental);
                    }
                }
            }

            return overdueRentals;
        }

        public async Task UpdateRentalOverdueStatus(Rental rental)
        {
            using (var connection = new SqlConnection(_connectionstring))
            {
                await connection.OpenAsync();

                // Update the OverdueStatus to true for the given rental
                var cmd = new SqlCommand("UPDATE Rental SET OverdueStatus = 1 WHERE RentalId = @rentalId", connection);

                cmd.Parameters.AddWithValue("@rentalId", rental.RentalId);

                await cmd.ExecuteNonQueryAsync();
            }
        }




    }
}
