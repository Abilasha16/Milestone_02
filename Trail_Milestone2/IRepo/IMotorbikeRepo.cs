using Trail_Milestone2.Entity;

namespace Trail_Milestone2.IRepo
{
    public interface IMotorbikeRepo
    {
        Task<MotorBike> AddBike(MotorBike motorBike);
        Task<MotorBike> EditBike(Guid Id);
        Task<MotorBike> UpdateBike(MotorBike motorBike);
        Task<List<MotorBike>> GetAllBike();
        Task<MotorBike> DeleteBike(Guid id);
        Task<MotorBike> GetById(Guid id);
        Task<MotorBike> GetRegisterNumber(string registerno);
        Task<List<MotorBike>> Get6Bikes();
    }
}
