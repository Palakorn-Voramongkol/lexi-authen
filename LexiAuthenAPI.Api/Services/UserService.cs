// LexiAuthenAPI.Api/Services/UserService.cs
using AutoMapper;
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Api.DTOs.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexiAuthenAPI.Infrastructure.Repositories;

namespace LexiAuthenAPI.Api.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        // Optionally, inject a password hasher service
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IPermissionRepository permissionRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
            
        }

        public async Task<IEnumerable<ReadUserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadUserDto>>(users);
        }

        public async Task<ReadUserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return null;

            return _mapper.Map<ReadUserDto>(user);
        }

        public async Task<ReadUserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var user = _mapper.Map<User>(createUserDto);
            user.PasswordHash = _passwordHasher.HashPassword(createUserDto.Password);

            // Optionally, assign roles if RoleIds are provided
            // user.UserRoles = createUserDto.RoleIds.Select(roleId => new UserRole { RoleId = roleId }).ToList();

            var createdUser = await _userRepository.AddAsync(user);
            return _mapper.Map<ReadUserDto>(createdUser);
        }


        public async Task<ReadUserDto> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            // Use AutoMapper to map the DTO to the existing entity, only updating provided fields
            _mapper.Map(updateUserDto, user);

            // Optionally handle the password update separately if hashing is required
            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                user.PasswordHash = _passwordHasher.HashPassword(updateUserDto.Password);
            }

            await _userRepository.UpdateAsync(user);

            // Return the updated user as a ReadUserDto
            var updatedUserDto = _mapper.Map<ReadUserDto>(user);
            return updatedUserDto;
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            await _userRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> AddUserRoleAsync(AddUserRoleDto addUserRoleDto)
        {
            var user = await _userRepository.GetByIdAsync(addUserRoleDto.UserId);
            if (user == null) return false;

            var role = await _roleRepository.GetByIdAsync(addUserRoleDto.RoleId);
            if (role == null) return false;

            user.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> RemoveUserRoleAsync(RemoveUserRoleDto removeUserRoleDto)
        {
            var user = await _userRepository.GetByIdAsync(removeUserRoleDto.UserId);
            if (user == null) return false;

            var userRole = user.UserRoles.FirstOrDefault(ur => ur.RoleId == removeUserRoleDto.RoleId);
            if (userRole == null) return false;

            user.UserRoles.Remove(userRole);
            await _userRepository.UpdateAsync(user);
            return true;
        }


        public async Task<bool> AddPermissionUserAsync(AddPermissionUserDto addPermissionUserDto)
        {
            var user = await _userRepository.GetByIdAsync(addPermissionUserDto.UserId);
            if (user == null) return false;

            var permission = await _permissionRepository.GetByIdAsync(addPermissionUserDto.PermissionId);
            if (permission == null) return false;

            user.UserPermissions.Add(new UserPermission { UserId = user.Id, PermissionId = permission.Id });
            await _userRepository.UpdateAsync(user);
            return true;
        }

        public async Task<bool> RemovePermissionUserAsync(RemovePermissionUserDto removePermissionUserDto)
        {
            var user = await _userRepository.GetByIdWithPermissionUserAsync(removePermissionUserDto.UserId);
            if (user == null || user.UserPermissions == null || !user.UserPermissions.Any()) return false;

            var userPermission = user.UserPermissions.FirstOrDefault(up => up.PermissionId == removePermissionUserDto.PermissionId);
            if (userPermission == null) return false;

            user.UserPermissions.Remove(userPermission);
            await _userRepository.UpdateAsync(user);
            return true;
        }
    }
}
