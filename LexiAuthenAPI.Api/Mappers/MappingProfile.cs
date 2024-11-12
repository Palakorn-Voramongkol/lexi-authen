// src/LexiAuthenAPI.Api/Mappers/MappingProfile.cs
using AutoMapper;
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Api.Models.Auth;
using LexiAuthenAPI.Api.DTOs.Role;
using LexiAuthenAPI.Api.DTOs.Permission;

namespace LexiAuthenAPI.Api.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Map AuthDto to User
            CreateMap<RegisterDto, User>();
            CreateMap<LoginDto, User>();
            
            // Map RoleDto to Role
            CreateMap<CreateRoleDto, Role>();
            CreateMap<UpdateRoleDto, Role>();

            // Map PermissionDto to Permission
            CreateMap<CreatePermissionDto, Permission>();
            CreateMap<UpdatePermissionDto, Permission>();
        }
    }
}
