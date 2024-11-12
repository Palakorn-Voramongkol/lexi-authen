// LexiAuthenAPI.Api/Configuration/MappingProfile.cs
using AutoMapper;
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Api.DTOs.User;
using LexiAuthenAPI.Api.DTOs.Auth;
using LexiAuthenAPI.Api.DTOs.Role;
using LexiAuthenAPI.Api.DTOs.Permission;
using LexiAuthenAPI.Api.DTOs.Uiitem;
using LexiAuthenAPI.Api.DTOs.Uipermission;


namespace LexiAuthenAPI.Api.Configuration
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // User Mappings
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
            CreateMap<User, ReadUserDto>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.Role)));
            CreateMap<UpdateUserDto, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

            // Role Mappings
            CreateMap<Role, ReadRoleDto>().ReverseMap();
            CreateMap<Role, CreateRoleDto>().ReverseMap();
            CreateMap<Role, UpdateRoleDto>().ReverseMap();

            // Permission Mappings
            CreateMap<Permission, ReadPermissionDto>().ReverseMap();
            CreateMap<Permission, CreatePermissionDto>().ReverseMap();
            CreateMap<Permission, UpdatePermissionDto>().ReverseMap();

            // Uiitem Mappings
            CreateMap<Uiitem, ReadUiitemDto>().ReverseMap();
            CreateMap<Uiitem, CreateUiitemDto>().ReverseMap();
            CreateMap<Uiitem, UpdateUiitemDto>().ReverseMap();

            // Uipermission Mappings
            CreateMap<Uipermission, ReadUipermissionDto>().ReverseMap();
            CreateMap<Uipermission, CreateUipermissionDto>().ReverseMap();
            CreateMap<Uipermission, UpdateUipermissionDto>().ReverseMap();

           
        }
    }
}
