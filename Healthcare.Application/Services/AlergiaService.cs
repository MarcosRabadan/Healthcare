using AutoMapper;
using Healthcare.Application.DTOs;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Healthcare.Application.Services
{
    public class AlergiaService
    {
        private readonly IAlergiaRepository _alergiaRepository;
        private readonly IMapper _mapper;

        public AlergiaService(IAlergiaRepository alergiaRepository, IMapper mapper)
        {
            _alergiaRepository = alergiaRepository;
            _mapper = mapper;
        }

        public async Task<AlergiaDto?> GetByIdAsync(int id)
        {
            var alergia = await _alergiaRepository.GetByIdAsync(id);
            return alergia == null ? null : _mapper.Map<AlergiaDto>(alergia);
        }

        public async Task<IEnumerable<Alergia>> GetAllAsync()
        {
            return await _alergiaRepository.GetAllAsync();
        }

        public async Task<Alergia> CreateAsync(AlergiaDto alergiaDto)
        {
            var alergia = _mapper.Map<Alergia>(alergiaDto);
            await _alergiaRepository.AddAsync(alergia);
            await _alergiaRepository.SaveChangesAsync();
            return alergia;
        }

        public async Task<bool> UpdateAsync(int id, AlergiaDto alergia)
        {
            var existing = await _alergiaRepository.GetByIdAsync(id);
            if (existing == null)
                return false;

            _mapper.Map(alergia, existing);

            _alergiaRepository.Update(existing);
            await _alergiaRepository.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var alergia = await _alergiaRepository.GetByIdAsync(id);

            if (alergia == null)
                return false;

            alergia.IsDeleted = true;
            _alergiaRepository.Update(alergia);

            await _alergiaRepository.SaveChangesAsync();
            return true;
        }
    }
}