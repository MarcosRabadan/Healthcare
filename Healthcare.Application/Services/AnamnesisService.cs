using AutoMapper;
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

        public async Task<IEnumerable<Anamnesis>> GetAllAsync()
        {
            return await _anamnesisRepository.GetAllAsync();
        }

        public async Task<Anamnesis?> GetByIdAsync(int id)
        {
            return await _anamnesisRepository.GetByIdAsync(id);
        }

        public async Task<Anamnesis> CreateAsync(Anamnesis anamnesis)
        {
            await _anamnesisRepository.AddAsync(anamnesis);
            await _anamnesisRepository.SaveChangesAsync();
            return anamnesis;
        }

        public async Task<bool> UpdateAsync(int id, Anamnesis anamnesis)
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