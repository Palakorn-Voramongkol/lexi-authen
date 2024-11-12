// LexiAuthenAPI.Api/Services/UiitemService.cs
using AutoMapper;
using LexiAuthenAPI.Domain.Entities;
using LexiAuthenAPI.Domain.Interfaces;
using LexiAuthenAPI.Api.DTOs.Uiitem;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LexiAuthenAPI.Api.Services
{
    public class UiitemService : IUiitemService
    {
        private readonly IUiitemRepository _uiitemRepository;
        private readonly IMapper _mapper;

        public UiitemService(IUiitemRepository uiitemRepository, IMapper mapper)
        {
            _uiitemRepository = uiitemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadUiitemDto>> GetAllUiitemsAsync()
        {
            var uiitems = await _uiitemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ReadUiitemDto>>(uiitems);
        }

        public async Task<ReadUiitemDto> GetUiitemByIdAsync(int id)
        {
            var uiitem = await _uiitemRepository.GetByIdAsync(id);
            if (uiitem == null)
            {
                return null;
            }

            return _mapper.Map<ReadUiitemDto>(uiitem);
        }

        public async Task<ReadUiitemDto> CreateUiitemAsync(CreateUiitemDto createUiitemDto)
        {
            var uiitem = _mapper.Map<Uiitem>(createUiitemDto);
            var createdUiitem = await _uiitemRepository.AddAsync(uiitem);
            return _mapper.Map<ReadUiitemDto>(createdUiitem);
        }


        public async Task<ReadUiitemDto> UpdateUiitemAsync(int id, UpdateUiitemDto updateUiitemDto)
        {
            var existingUiitem = await _uiitemRepository.GetByIdAsync(id);
            if (existingUiitem == null)
            {
                throw new KeyNotFoundException("Uiitem not found.");
            }

            // Map the updated properties from DTO to the existing entity
            _mapper.Map(updateUiitemDto, existingUiitem);

            // Update the entity in the repository
            await _uiitemRepository.UpdateAsync(existingUiitem);

            // Return the updated entity mapped to a ReadUiitemDto
            return _mapper.Map<ReadUiitemDto>(existingUiitem);
        }


        public async Task<bool> DeleteUiitemAsync(int id)
        {
            var uiitem = await _uiitemRepository.GetByIdAsync(id);
            if (uiitem == null)
            {
                return false;
            }

            await _uiitemRepository.DeleteAsync(id);
            return true;
        }




    }
}
