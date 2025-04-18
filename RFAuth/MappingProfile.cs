﻿using AutoMapper;
using RFAuth.DTO;
using RFAuth.Entities;

namespace RFAuth
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<User, UserResponse>();
            CreateMap<User, UserAttributes>();
            CreateMap<UserAttributes, UserResponse>();
            CreateMap<LoginData, LoginResponse>();
            CreateMap<Session, SessionDTO>();
        }
    }
}
