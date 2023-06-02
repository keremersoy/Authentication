
using Data_Access_Layer.DTO;
using Data_Access_Layer.Entities;

namespace Business_Logic_Layer.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User_DTO>> GetUser();
        Task<bool> Register(User_DTO request);
        Task<string> Login(User_DTO request);
    }
}