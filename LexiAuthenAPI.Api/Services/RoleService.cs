// LexiAuthenAPI.Api/Services/RoleService.cs
using AutoMapper;
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Api.DTOs.Role;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LexiAuthenAPI.Infrastructure.Repositories;

namespace LexiAuthenAPI.Api.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IMapper _mapper;

    public RoleService(IRoleRepository roleRepository, IUserRepository userRepository, IPermissionRepository permissionRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _userRepository = userRepository;
            _permissionRepository = permissionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadRoleDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadRoleDto>>(roles);
        }

        public async Task<ReadRoleDto> GetRoleByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
            {
                return null;
            }

            return _mapper.Map<ReadRoleDto>(role);
        }

        public async Task<ReadRoleDto> CreateRoleAsync(CreateRoleDto createRoleDto)
        {
            var role = _mapper.Map<Role>(createRoleDto);
            var createdRole = await _roleRepository.AddAsync(role);
            return _mapper.Map<ReadRoleDto>(createdRole);
        }

        public async Task<ReadRoleDto> UpdateRoleAsync(int id, UpdateRoleDto updateRoleDto)
        {
            var existingRole = await _roleRepository.GetByIdAsync(id);
            if (existingRole == null)
            {
                throw new KeyNotFoundException("Role not found.");
            }

            // Use AutoMapper to map the update DTO to the existing entity
            _mapper.Map(updateRoleDto, existingRole);

            // Update the repository
            await _roleRepository.UpdateAsync(existingRole);

            // Map the updated role to a ReadRoleDto and return it
            var updatedRoleDto = _mapper.Map<ReadRoleDto>(existingRole);
            return updatedRoleDto;
        }

        public async Task<bool> DeleteRoleAsync(int id)
        {
            var existingRole = await _roleRepository.GetByIdAsync(id);
            if (existingRole == null)
            {
                return false; // Return false if the role doesn't exist
            }

            await _roleRepository.DeleteAsync(id);
            return true; // Return true to indicate successful deletion
        }


        public async Task<bool> AddUserToRoleAsync(AddUserToRoleDto addUserToRoleDto)
        {
            var role = await _roleRepository.GetByIdAsync(addUserToRoleDto.RoleId);
            if (role == null) return false;

            var user = await _userRepository.GetByIdAsync(addUserToRoleDto.UserId);
            if (user == null) return false;

            role.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
            await _roleRepository.UpdateAsync(role);
            return true;
        }

        public async Task<bool> RemoveUserFromRoleAsync(RemoveUserFromRoleDto removeUserFromRoleDto)
        {
            // Load the role including its UserRoles collection
            var role = await _roleRepository.GetByIdWithUserRolesAsync(removeUserFromRoleDto.RoleId);
            
            if (role == null || role.UserRoles == null || !role.UserRoles.Any()) return false;

            // Find the user role matching the user ID
            var userRole = role.UserRoles.FirstOrDefault(ur => ur.UserId == removeUserFromRoleDto.UserId);
            if (userRole == null) return false;

            // Remove the user role from the collection
            role.UserRoles.Remove(userRole);

            // Update the role in the repository
            await _roleRepository.UpdateAsync(role);

            return true;
        }

        public async Task<bool> AddPermissionToRoleAsync(AddPermissionToRoleDto addPermissionToRoleDto)
        {
            var role = await _roleRepository.GetByIdAsync(addPermissionToRoleDto.RoleId);
            if (role == null) return false;

            var permission = await _permissionRepository.GetByIdAsync(addPermissionToRoleDto.RoleId);
            if (permission == null) return false;

            role.RolePermissions.Add(new RolePermission { RoleId = role.Id, PermissionId = permission.Id });
            await _roleRepository.UpdateAsync(role);
            return true;
        }

        public async Task<bool> RemovePermissionFromRoleAsync(RemovePermissionFromRoleDto removePermissionFromRoleDto)
        {
            // Load the role including its RolePermissions collection
            var role = await _roleRepository.GetByIdWithPermissionRolesAsync(removePermissionFromRoleDto.RoleId);

            if (role == null || role.RolePermissions == null || !role.RolePermissions.Any()) return false;

            // Find the role-permission matching the permission ID
            var rolePermission = role.RolePermissions.FirstOrDefault(rp => rp.PermissionId == removePermissionFromRoleDto.PermissionId);
            if (rolePermission == null) return false;

            // Remove the role-permission from the collection
            role.RolePermissions.Remove(rolePermission);

            // Update the role in the repository
            await _roleRepository.UpdateAsync(role);

            return true;
        }



        Task<bool> IRoleService.DeleteRoleAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
