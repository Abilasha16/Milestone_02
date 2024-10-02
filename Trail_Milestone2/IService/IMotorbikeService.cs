using Trail_Milestone2.DTO.Reguest;
using Trail_Milestone2.DTO.Response;

namespace Trail_Milestone2.IService
{
    public interface IMotorbikeService
    {
        Task<MotorbikeResponse> AddBike(MotorbikeReguest motorbikeReguest/*, IFormFile imageFile*/);
        Task<MotorbikeResponse> EditBike(Guid id, MotorbikeReguest motorbikeReguest);
        Task<List<MotorbikeResponse>> GetAllBike();
        Task<MotorbikeResponse> DeleteBike(Guid id);
        Task<MotorbikeResponse> GetById(Guid id);
        Task<List<MotorbikeResponse>> Get6Bike();
    }
}
