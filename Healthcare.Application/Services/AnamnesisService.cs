using AutoMapper;
using Healthcare.Application.DTOs;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Healthcare.Application.Services
{
    public class AnamnesisService
    {
        private readonly IAnamnesisRepository _anamnesisRepository;
        private readonly IMapper _mapper;

        public AnamnesisService(IAnamnesisRepository anamnesisRepository, IMapper mapper)
        {
            _anamnesisRepository = anamnesisRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AnamnesisDto>> GetAllAsync()
        {
            var anamesis = await _anamnesisRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AnamnesisDto>>(anamesis);
        }

        public async Task<AnamnesisDto?> GetByIdAsync(int id)
        {
            var anamesis = await _anamnesisRepository.GetByIdAsync(id);
            return anamesis == null ? null : _mapper.Map<AnamnesisDto>(anamesis);
        }

        public async Task<AnamnesisDto> CreateAsync(AnamnesisDto anamnesisDto)
        {
            var anamnesis = _mapper.Map<Anamnesis>(anamnesisDto);
            await _anamnesisRepository.AddAsync(anamnesis);
            await _anamnesisRepository.SaveChangesAsync();
            return _mapper.Map<AnamnesisDto>(anamnesis);
        }

        public async Task<bool> UpdateAsync(int id, AnamnesisDto anamnesis)
        {
            var existing = await _anamnesisRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(anamnesis, existing);
            _anamnesisRepository.Update(existing);
            await _anamnesisRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var anamnesis = await _anamnesisRepository.GetByIdAsync(id);
            if (anamnesis == null)
                return false;

            anamnesis.IsDeleted = true;
            _anamnesisRepository.Update(anamnesis);
            await _anamnesisRepository.SaveChangesAsync();
            return true;
        }
    }
}