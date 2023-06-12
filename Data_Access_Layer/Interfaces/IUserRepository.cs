
using Data_Access_Layer.DTO;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUser();
        Task<bool> Register(User request);
        Task<User> Login(User_DTO request);
        Task<bool> CheckUserNameAsync(String username);
    }
}

