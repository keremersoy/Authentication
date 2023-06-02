using AutoMapper;
using Data_Access_Layer.DTO;
using Data_Access_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Logic_Layer.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, User_DTO>();
            CreateMap<User_DTO, User>();

        }
    }
}
