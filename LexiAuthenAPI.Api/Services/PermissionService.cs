// LexiAuthenAPI.Api/Services/PermissionService.cs
using AutoMapper;
using LexiAuthenAPI.Api.DTOs.Permission;
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Api.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUipermissionRepository _uipermissionRepository;
        private readonly IMapper _mapper;

        public PermissionService(IPermissionRepository permissionRepository, IUipermissionRepository uipermissionRepository, IMapper mapper)
        {
            _permissionRepository = permissionRepository;
            _uipermissionRepository = uipermissionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadPermissionDto>> GetAllPermissionsAsync()
        {
            var permissions = await _permissionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadPermissionDto>>(permissions);
        }

        public async Task<ReadPermissionDto> GetPermissionByIdAsync(int id)
        {
            var permission = await _permissionRepository.GetByIdAsync(id);
            if (permission == null)
            {
                return null;
            }

            return _mapper.Map<ReadPermissionDto>(permission);
        }

        public async Task<ReadPermissionDto> CreatePermissionAsync(CreatePermissionDto createPermissionDto)
        {
            var permission = _mapper.Map<Permission>(createPermissionDto);
            var createdPermission = await _permissionRepository.AddAsync(permission);
            return _mapper.Map<ReadPermissionDto>(createdPermission);
        }


        public async Task<ReadPermissionDto> UpdatePermissionAsync(int id, UpdatePermissionDto updatePermissionDto)
        {
            var existingPermission = await _permissionRepository.GetByIdAsync(id);
            if (existingPermission == null)
            {
                throw new KeyNotFoundException("Permission not found.");
            }

            // Use AutoMapper to map the update DTO to the existing entity
            _mapper.Map(updatePermissionDto, existingPermission);

            // Update the repository
            await _permissionRepository.UpdateAsync(existingPermission);

            // Map the updated permission to a ReadPermissionDto and return it
            var updatedPermissionDto = _mapper.Map<ReadPermissionDto>(existingPermission);
            return updatedPermissionDto;
        }


        public async Task<bool> DeletePermissionAsync(int id)
        {
            await _permissionRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> AddUipermissionPermissionAsync(AddUipermissionPermissionDto addUipermissionPermissionDto)
        {
            var permission = await _permissionRepository.GetByIdAsync(addUipermissionPermissionDto.PermissionId);
            if (permission == null) return false;

            var uipermission = await _uipermissionRepository.GetByIdAsync(addUipermissionPermissionDto.UipermissionId);
            if (uipermission == null) return false;

            permission.PermissionUipermissions.Add(new PermissionUipermission { PermissionId = permission.Id, UipermissionId = uipermission.Id });
            await _permissionRepository.UpdateAsync(permission);
            return true;
        }

        public async Task<bool> RemoveUipermissionPermissionAsync(RemoveUipermissionPermissionDto removeUipermissionPermissionDto)
        {
            var permission = await _permissionRepository.GetByIdWithPermissionUipermissionAsync(removeUipermissionPermissionDto.PermissionId);
            if (permission == null || permission.PermissionUipermissions == null || !permission.PermissionUipermissions.Any()) return false;

            var uipermissionPermission = permission.PermissionUipermissions.FirstOrDefault(up => up.UipermissionId == removeUipermissionPermissionDto.UipermissionId);
            if (uipermissionPermission == null) return false;

            permission.PermissionUipermissions.Remove(uipermissionPermission);
            await _permissionRepository.UpdateAsync(permission);
            return true;
        }

    }
}
