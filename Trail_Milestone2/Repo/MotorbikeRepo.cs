using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;
using Trail_Milestone2.Entity;
using Trail_Milestone2.IRepo;

namespace Trail_Milestone2.Repo
{
    public class MotorbikeRepo : IMotorbikeRepo
    {
        private readonly string _connectionstring;

        public MotorbikeRepo(string connectionstring)
        {
            _connectionstring = connectionstring;
        }

    //Add Bike
    //public async Task<MotorBike> AddBike(MotorBike motorBike)
    //{
    //    using (var connection = new SqlConnection(_connectionstring))
    //    {
    //        var command = new SqlCommand(
    //            "INSERT INTO Motorbike (MotorbikeId, RegisterNumber, Brand, Model, Category, ImageUrl, AvailabilityStatus) VALUES (@id, @registerno, @brand, @model, @category,@image, @status)", connection);

    //        command.Parameters.AddWithValue("@id", motorBike.MotorbikeId);
    //        command.Parameters.AddWithValue("@registerno", motorBike.RegisterNumber);
    //        command.Parameters.AddWithValue("@brand", motorBike.Brand);
    //        command.Parameters.AddWithValue("@model", motorBike.Model);
    //        command.Parameters.AddWithValue("@category", motorBike.Category);
    //        command.Parameters.AddWithValue("@image", motorBike.ImageUrl);
    //        command.Parameters.AddWithValue("@status", motorBike.AvailabilityStatus);

    //        await connection.OpenAsync();
    //        await command.ExecuteNonQueryAsync();
    //    }

    //    return motorBike;
    //}

    //add bike mutible image
    public async Task<MotorBike> AddBike(MotorBike motorBike)
    {
      using (var connection = new SqlConnection(_connectionstring))
      {
        var command = new SqlCommand(
            "INSERT INTO Motorbike (MotorbikeId, RegisterNumber, Brand, Model, Category, ImageUrl, AvailabilityStatus) " +
            "VALUES (@id, @registerno, @brand, @model, @category, @images, @status)", connection);

        command.Parameters.AddWithValue("@id", motorBike.MotorbikeId);
        command.Parameters.AddWithValue("@registerno", motorBike.RegisterNumber);
        command.Parameters.AddWithValue("@brand", motorBike.Brand);
        command.Parameters.AddWithValue("@model", motorBike.Model);
        command.Parameters.AddWithValue("@category", motorBike.Category);
        command.Parameters.AddWithValue("@images", motorBike.ImageUrl);  // Comma-separated image URLs
        command.Parameters.AddWithValue("@status", motorBike.AvailabilityStatus);

        await connection.OpenAsync();
        await command.ExecuteNonQueryAsync();
      }

      return motorBike;
    }


    //Edit Bike
    public async Task<MotorBike> EditBike(Guid Id)
        {
            MotorBike motorBike = null;

            using (var connection = new SqlConnection(_connectionstring))
            {
                var command = new SqlCommand("SELECT * FROM Motorbike WHERE MotorbikeId=@id", connection);

                command.Parameters.AddWithValue("@id", Id);
                await connection.OpenAsync();

                using (var command2 = await command.ExecuteReaderAsync())
                {
                    if (await command2.ReadAsync())
                    {
                        motorBike = new MotorBike()
                        {
                            MotorbikeId = command2.GetGuid(0),
                            RegisterNumber = command2.GetString(1),
                            Brand = command2.GetString(2),
                            Model = command2.GetString(3),
                            Category = command2.GetString(4),
                            ImageUrl = command2.GetString(5),
                            AvailabilityStatus = command2.GetBoolean(6)
                        };
                    }
                }
            }
            return motorBike;
        }

        public async Task<MotorBike> UpdateBike(MotorBike motorBike)
        {
            MotorBike updatebike = null;

            using (var connection = new SqlConnection(_connectionstring))
            {
                var command = new SqlCommand(
                    "UPDATE Motorbike SET RegisterNumber =@registerno, Brand = @brand, Model =@model, Category = @category ,AvailabilityStatus =@status WHERE MotorbikeId = @id  ", connection);
                command.Parameters.AddWithValue("@registerno", motorBike.RegisterNumber);
                command.Parameters.AddWithValue("@id", motorBike.MotorbikeId);
                command.Parameters.AddWithValue("@brand", motorBike.Brand);
                command.Parameters.AddWithValue("@model", motorBike.Model);
                command.Parameters.AddWithValue("@category", motorBike.Category);
                command.Parameters.AddWithValue("@status", motorBike.AvailabilityStatus);

                await connection.OpenAsync();

                var changeRow = await command.ExecuteNonQueryAsync();

                if (changeRow > 0)
                {
                    updatebike = motorBike;
                }

            }
            return updatebike;
        }

        //Get All Bike

        public async Task<List<MotorBike>> GetAllBike()
        {
            var bike = new List<MotorBike>();

            using (var connection = new SqlConnection(_connectionstring))
            {
                using (var cmd = new SqlCommand("SELECT * FROM Motorbike", connection))
                {
                    await connection.OpenAsync();
                    using (var cmd2 = await cmd.ExecuteReaderAsync())
                    {
                        while (cmd2.Read())
                        {
                            var data = new MotorBike
                            {
                                MotorbikeId = cmd2.GetGuid(cmd2.GetOrdinal("MotorbikeId")),
                                RegisterNumber = cmd2.GetString(cmd2.GetOrdinal("RegisterNumber")),
                                Brand = cmd2.GetString(cmd2.GetOrdinal("Brand")),
                                Model = cmd2.GetString(cmd2.GetOrdinal("Model")),
                                Category = cmd2.GetString(cmd2.GetOrdinal("Category")),
                                ImageUrl = cmd2.GetString(cmd2.GetOrdinal("ImageUrl")),
                                AvailabilityStatus = Convert.ToBoolean(cmd2["AvailabilityStatus"])

                            };
                            bike.Add(data);
                        }
                    }
                }
            }
            return bike;
        }

        //Delete Bike

        public async Task<MotorBike> DeleteBike(Guid id)
        {
            MotorBike bike = null;
            using(var connection = new SqlConnection(_connectionstring))
            {
                var cmd = new SqlCommand("SELECT * FROM Motorbike WHERE MotorbikeId = @id",connection);
                cmd.Parameters.AddWithValue("@id", id);

                await connection.OpenAsync();

                using(var  cmd2 = await cmd.ExecuteReaderAsync())
                {
                    if (cmd2.Read())
                    {
                        bike = new MotorBike
                        {
                            MotorbikeId = cmd2.GetGuid(0),
                            RegisterNumber = cmd2.GetString(1),
                            Brand = cmd2.GetString(2),
                            Model = cmd2.GetString(3),
                            Category = cmd2.GetString(4),
                            AvailabilityStatus = Convert.ToBoolean(cmd2["AvailabilityStatus"])
                        };
                    }

                }
                if(bike != null)
                {
                    var deletecmd = new SqlCommand("DELETE FROM Motorbike WHERE MotorbikeId = @id", connection);
                    deletecmd.Parameters.AddWithValue("@id",id);

                    await deletecmd.ExecuteReaderAsync();
                }
                return bike;

            }
        }

        // Get By Id

        public async Task<MotorBike> GetById(Guid id)
        {
            MotorBike bike = null;
            using( var connection = new SqlConnection(_connectionstring))
            {
                var cmd = new SqlCommand("SELECT * FROM Motorbike WHERE MotorbikeId = @id", connection);
                cmd.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();

                using(var conn = await cmd.ExecuteReaderAsync())
                {
                    if (conn.Read())
                    {
                        bike = new MotorBike
                        {
                            MotorbikeId = conn.GetGuid(0),
                            RegisterNumber = conn.GetString(1),
                            Brand = conn.GetString(2),
                            Model = conn.GetString(3),
                            Category = conn.GetString(4),
                            ImageUrl = conn.GetString(5),
                            AvailabilityStatus = Convert.ToBoolean(conn["AvailabilityStatus"])
                        };
                    }
                }
                return bike;
            }
        }

        // Get Register Number

        public async Task<MotorBike> GetRegisterNumber(string registerno)
        {
            MotorBike bike = null;

            using(var connection = new SqlConnection(_connectionstring))
            {
                var cmd = new SqlCommand("select * from Motorbike where RegisterNumber = @registerno", connection);
                cmd.Parameters.AddWithValue("@registerno", registerno);

                await connection.OpenAsync();
                using(var reader = await cmd.ExecuteReaderAsync())
                {
                    if (reader.Read())
                    {
                        bike = new MotorBike
                        {
                            MotorbikeId=reader.GetGuid(0),
                            RegisterNumber = reader.GetString(1),
                            Brand = reader.GetString(2),
                            Model = reader.GetString(3),
                            Category = reader.GetString(4),
                            ImageUrl = reader.GetString(5),
                            AvailabilityStatus = Convert.ToBoolean(reader["AvailabilityStatus"])
                        };
                    }
                }
                return bike;
            }
        }

        //Get Just 6 Bikes 
        public async Task<List<MotorBike>> Get6Bikes()
        {
            var bikes = new List<MotorBike>();

            using(var connection = new SqlConnection(_connectionstring))
            {
               using (var cmd = new SqlCommand("SELECT TOP 6 * FROM Motorbike", connection))
                {
                    await connection.OpenAsync();
                    using(var cmd2 = await cmd.ExecuteReaderAsync())
                    {
                         while (cmd2.Read())
                        {
                            var data = new MotorBike()
                            {
                                MotorbikeId = cmd2.GetGuid(0),
                                RegisterNumber = cmd2.GetString(1),
                                Brand = cmd2.GetString(2),
                                Model = cmd2.GetString(3),
                                Category = cmd2.GetString(4),
                                ImageUrl = cmd2.GetString(5),
                                AvailabilityStatus = Convert.ToBoolean(cmd2["AvailabilityStatus"])
                            };
                            bikes.Add(data);
                        }
                    }
                }

            }
            return bikes;
        }


    }
}
