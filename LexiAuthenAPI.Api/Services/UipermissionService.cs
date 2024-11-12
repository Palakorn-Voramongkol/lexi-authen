// LexiAuthenAPI.Api/Services/UipermissionService.cs
using AutoMapper;
using LexiAuthenAPI.Api.DTOs.Uipermission;
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Api.Services
{
    public class UipermissionService : IUipermissionService
    {

        private readonly IMapper _mapper;
        private readonly IUipermissionRepository _uipermissionRepository;
        private readonly IUiitemRepository _uiitemRepository;

        public UipermissionService(IUipermissionRepository uipermissionRepository, IUiitemRepository uiitemRepository, IMapper mapper)
        {
            _uipermissionRepository = uipermissionRepository;
            _uiitemRepository = uiitemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadUipermissionDto>> GetAllUipermissionsAsync()
        {
            var uipermissions = await _uipermissionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadUipermissionDto>>(uipermissions);
        }


        public async Task<ReadUipermissionDto> GetUipermissionByIdAsync(int id)
        {
            var uipermission = await _uipermissionRepository.GetByIdAsync(id);
            if (uipermission == null)
            {
                throw new KeyNotFoundException("Uipermission not found.");
            }

            // Map the entity to ReadUipermissionDto
            return _mapper.Map<ReadUipermissionDto>(uipermission);
        }


        public async Task<ReadUipermissionDto> CreateUipermissionAsync(CreateUipermissionDto createUipermissionDto)
        {
            // Map the DTO to the entity
            var uipermission = _mapper.Map<Uipermission>(createUipermissionDto);

            // Add the entity to the repository
            var createdUipermission = await _uipermissionRepository.AddAsync(uipermission);

            // Return the created entity as a ReadUipermissionDto
            return _mapper.Map<ReadUipermissionDto>(createdUipermission);
        }



        public async Task<ReadUipermissionDto> UpdateUipermissionAsync(int id, UpdateUipermissionDto updateUipermissionDto)
        {
            var existingUipermission = await _uipermissionRepository.GetByIdAsync(id);
            if (existingUipermission == null)
            {
                throw new KeyNotFoundException("Uipermission not found.");
            }

            // Map the incoming DTO to the existing entity
            _mapper.Map(updateUipermissionDto, existingUipermission);

            // Update the entity in the repository
            await _uipermissionRepository.UpdateAsync(existingUipermission);

            // Return the updated entity as a ReadUipermissionDto
            return _mapper.Map<ReadUipermissionDto>(existingUipermission);
        }


        public async Task<bool> DeleteUipermissionAsync(int id)
        {
            await _uipermissionRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> AddUiitemUipermissionAsync(AddUiitemUipermissionDto addUiitemUipermissionDto)
        {
            var uipermission = await _uipermissionRepository.GetByIdAsync(addUiitemUipermissionDto.UipermissionId);
            if (uipermission == null) return false;

            var uiitem = await _uiitemRepository.GetByIdAsync(addUiitemUipermissionDto.UiitemId);
            if (uiitem == null) return false;

            uipermission.UipermissionUiitems.Add(new UipermissionUiitem { UipermissionId = uipermission.Id, UiitemId = uiitem.Id });
            await _uipermissionRepository.UpdateAsync(uipermission);
            return true;
        }

        public async Task<bool> RemoveUiitemUipermissionAsync(RemoveUiitemUipermissionDto removeUiitemUipermissionDto)
        {
            // Load the uipermission including its UipermissionUiitems collection
            var uipermission = await _uipermissionRepository.GetByIdWithUiitemsAsync(removeUiitemUipermissionDto.UipermissionId);

            if (uipermission == null || uipermission.UipermissionUiitems == null || !uipermission.UipermissionUiitems.Any()) return false;

            // Find the uipermission-uiitem matching the uiitem ID
            var uipermissionUiitem = uipermission.UipermissionUiitems.FirstOrDefault(ui => ui.UiitemId == removeUiitemUipermissionDto.UiitemId);
            if (uipermissionUiitem == null) return false;

            // Remove the uipermission-uiitem from the collection
            uipermission.UipermissionUiitems.Remove(uipermissionUiitem);

            // Update the uipermission in the repository
            await _uipermissionRepository.UpdateAsync(uipermission);

            return true;
        }

    }
}
