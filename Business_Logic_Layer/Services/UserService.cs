using AutoMapper;
using Data_Access_Layer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using Data_Access_Layer.Interfaces;
using Business_Logic_Layer.Interfaces;
using Data_Access_Layer.DTO;
using System.Text;
using Business_Logic_Layer.Validations;
using FluentValidation;

namespace Business_Logic_Layer.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UserService(IMapper mapper, IUserRepository userRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _config = config;
        }

        public async Task<IEnumerable<User_DTO>> GetUser()
        {
            var res = await _userRepository.GetUser();
            return _mapper.Map<List<User_DTO>>(res);
        }
        public async Task<bool> Register(User_DTO request)
        {
            var isTheUsernameUnique=await _userRepository.CheckUserNameAsync(request.UserName);
            if(isTheUsernameUnique)
            {
                throw new ValidationException("Bu kullanıcı adı başkası tarafından kullanılmakta.");
            }

            RegisterValidator validator = new RegisterValidator();
            validator.ValidateAndThrow(request);

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                UserName = request.UserName,
                Name = request.Name,
                Surname = request.Surname,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            if (await _userRepository.Register(user) == null)
            {
                return false;
            }
            return true;
        }


        public async Task<string> Login(User_DTO request)
        {

            var validator = new LoginValidator();
            validator.ValidateAndThrow(request);

            var isThereUser = await _userRepository.CheckUserNameAsync(request.UserName);
            if (!isThereUser)
            {
                throw new ValidationException("Kullanıcı bulunamadı...");
            }

            var user = await _userRepository.Login(request);
            string token = CreateToken(user);
            return token;
        }

        private string CreateToken(User user)
        {
                List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.UserName)
            };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
                var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(5),
                    signingCredentials: cred
                    );
                var jwt = new JwtSecurityTokenHandler().WriteToken(token);
                return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    }
}
