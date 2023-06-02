using Data_Access_Layer.DTO;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDatabaseContext _context;

        public UserRepository(IDatabaseContext context,
            IConfiguration configuration)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUser()
        {
            try
            {

                //var res =await _context.Users.ToListAsync();
                var res = await _context.Users.ToListAsync();
                return res;
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
                return null;
            }
        }

        public async Task<bool> Register(User request)
        {
            try
            {

                var result = await _context.AddAsync(request);
                if (result != null)
                {
                    var inserted = await _context.SaveChangesAsync();
                    return inserted == 1;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
        public async Task<User> Login(User_DTO request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x =>
                x.UserName == request.UserName
            );

            var pass = Encoding.Default.GetString(VerifyPasswordHash(request.Password, user.PasswordSalt));
            var userpass = Encoding.Default.GetString(user.PasswordHash);

            if (pass.Equals(userpass))
            {
                return user;
            }
            return null;
        }
        public byte[] VerifyPasswordHash(string password, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash;
            }
        }
        public async Task<bool>  CheckUserName(String username) {
            var result=await _context.Users.AnyAsync(x => x.UserName == username);
            return !result;
        }
    }
}
