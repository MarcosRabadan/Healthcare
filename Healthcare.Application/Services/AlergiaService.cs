using AutoMapper;
using Healthcare.Application.Constants;
using Healthcare.Application.DTOs.Responses;
using Healthcare.Application.DTOs.Requests;
using Healthcare.Domain.Entities;
using Healthcare.Domain.Enums;
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

        public async Task<AlergiaResponseDto?> GetByIdAsync(int id)
        {
            var alergia = await _alergiaRepository.GetByIdAsync(id);
            return alergia == null ? null : _mapper.Map<AlergiaResponseDto>(alergia);
        }

        public async Task<IEnumerable<AlergiaResponseDto>> GetAllAsync()
        {
            var alergia = await _alergiaRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<AlergiaResponseDto>>(alergia.ToList());
        }

        public async Task<(AlergiaResponseDto? Created, ErrorResponseDto? Error)> CreateAsync(AlergiaRequestDto alergiaDto)
        {
            if (!Enum.IsDefined(typeof(TipoAlergia), alergiaDto.Tipo.Value))            
                return (null, ErrorMessages.TipoAlergiaInvalido);
            

            var alergia = _mapper.Map<Alergia>(alergiaDto);

            await _alergiaRepository.AddAsync(alergia);
            await _alergiaRepository.SaveChangesAsync();

            var createdDto = _mapper.Map<AlergiaResponseDto>(alergia);
            return (createdDto, null);
        }

        public async Task<bool> UpdateAsync(int id, AlergiaRequestDto alergia)
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